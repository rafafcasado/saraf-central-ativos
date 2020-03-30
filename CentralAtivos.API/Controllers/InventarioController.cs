using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class InventarioController : ApiController
    {
        private readonly ICentroCusto _centroCustoRepository;
        private readonly IContaContabil _contaContabilRepository;
        private readonly IEmpresa _empresaRepository;
        private readonly IEspecie _especieRepository;
        private readonly IEspeciePropriedade _especiePropriedadeRepository;
        private readonly IFilial _filialDbRepository;
        private readonly IGrupo _grupoRepository;
        private readonly IInventario _repository;
        private readonly IInventarioConfig _configRepository;
        private readonly IInventarioFilial _filialRepository;
        private readonly IInventarioLocal _localRepository;
        private readonly IInventarioUsuario _usuarioRepository;
        private readonly IItem _itemRepository;
        private readonly IItemEstado _itemEstadoRepository;
        private readonly ILocal _localDbRepository;
        private readonly IPropriedade _propriedadeRepository;
        private readonly IResponsavel _responsavelRepository;

        public InventarioController(IInventario repository, IInventarioFilial filialRepository, IInventarioLocal localRepository, IInventarioUsuario usuarioRepository, IInventarioConfig configRepository, IGrupo grupoRepository, IEspecie especieRepository, IEspeciePropriedade especiePropriedadeRepository, IResponsavel responsavelRepository, ICentroCusto centroCustoRepository, IContaContabil contaContabilRepository, IItem itemRepository, IPropriedade propriedadeRepository, IEmpresa empresaRepository, ILocal localDbRepository, IFilial filialDbRepository, IItemEstado itemEstadoRepository)
        {
            _repository = repository;
            _filialRepository = filialRepository;
            _localRepository = localRepository;
            _usuarioRepository = usuarioRepository;
            _configRepository = configRepository;
            _grupoRepository = grupoRepository;
            _especieRepository = especieRepository;
            _especiePropriedadeRepository = especiePropriedadeRepository;
            _responsavelRepository = responsavelRepository;
            _centroCustoRepository = centroCustoRepository;
            _contaContabilRepository = contaContabilRepository;
            _itemRepository = itemRepository;
            _propriedadeRepository = propriedadeRepository;
            _empresaRepository = empresaRepository;
            _localDbRepository = localDbRepository;
            _filialDbRepository = filialDbRepository;
            _itemEstadoRepository = itemEstadoRepository;
        }

        /// <summary>
        /// Retorna Lista de Inventários Cadastrados para a Empresa de acordo com o ID informado
        /// </summary>
        /// <param name="empresaID">ID da Empresa</param>
        [HttpGet]
        [Route("api/inventario/getByEmpresaID/{empresaID}")]
        public IHttpActionResult GetByEmpresaID(int empresaID)
        {
            try
            {
                return Ok(_repository.GetByEmpresaID(empresaID));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Inventário relacionado ao ID informado
        /// </summary>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var inventario = _repository.GetByID(id);

                if (inventario == null)
                    return NotFound();

                var configs = from c in _configRepository.GetByInventarioID(inventario.ID)
                              select new { c.ID, c.EntidadeCampo.NomeCampo, Entidade = c.EntidadeCampo.Entidade.ToString(), c.Obrigatorio, c.Visivel };

                return Ok(new { inventario.AplicarMascara, inventario.Codigo, inventario.DataCadastroFormatada, inventario.EmpresaID, inventario.Geral, inventario.ID, inventario.MascaraNomeItem, inventario.Nome, inventario.StatusID, Configs = configs }); ;
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui um Inventário
        /// </summary>
        /// <param name="inventario">Objeto do tipo Inventario</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] Inventario inventario)
        {
            try
            {
                var usuarios = inventario.Usuarios;

                inventario.Usuarios = null;

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _repository.Insert(inventario);

                foreach (var filial in inventario.Filiais)
                {

                    _filialRepository.Insert(new InventarioFilial()
                    {
                        FilialID = filial.ID,
                        InventarioID = inventario.ID
                    });
                }

                if (inventario.Geral)
                {
                    foreach (var filial in inventario.Filiais)
                    {
                        var locaisFilial = _localDbRepository.GetByFilialID(filial.ID);

                        foreach (var local in locaisFilial)
                        {
                            _localRepository.Insert(new InventarioLocal()
                            {
                                LocalID = local.ID,
                                InventarioID = inventario.ID
                            });
                        }
                    }
                }
                else
                {
                    foreach (var local in inventario.Locais)
                    {
                        _localRepository.Insert(new InventarioLocal()
                        {
                            LocalID = local.ID,
                            InventarioID = inventario.ID
                        });
                    }
                }

                foreach (var usuario in usuarios)
                {
                    usuario.InventarioID = inventario.ID;

                    _usuarioRepository.Insert(usuario);
                }

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de um Inventário
        /// </summary>
        /// <param name = "id" > ID do Inventário que será atualizado</param>
        /// <param name="inventario">Objeto do tipo Inventario</param>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Inventario inventario)
        {
            try
            {
                var inventarioDB = _repository.GetByID(id);

                if (inventarioDB == null)
                    return NotFound();

                inventarioDB.AplicarMascara = inventario.AplicarMascara;
                inventarioDB.MascaraNomeItem = inventario.MascaraNomeItem;

                _repository.Update(inventarioDB);

                foreach (var config in inventario.Configs)
                {
                    var configDB = _configRepository.GetByID(config.ID);

                    if (configDB != null)
                    {
                        configDB.Obrigatorio = config.Obrigatorio;
                        configDB.Visivel = config.Visivel;

                        _configRepository.Update(configDB);
                    }
                }

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Carga Inicial do Inventário
        /// </summary>
        /// <param name="inventarioID">ID do Inventário</param>
        [HttpGet, Route("api/inventario/cargaInicial/{inventarioID}")]
        public IHttpActionResult CargaInicial(int inventarioID)
        {
            try
            {
                var inventario = _repository.GetByID(inventarioID);

                var empresa = _empresaRepository.GetByID(inventario.EmpresaID);

                var grupos = _grupoRepository.GetByEmpresaID(inventario.EmpresaID);

                var especies = _especieRepository.GetByEmpresaID(inventario.EmpresaID);

                var propriedades = _propriedadeRepository.GetByEmpresaID(inventario.EmpresaID);

                var especiePropriedades = _especiePropriedadeRepository.GetByEmpresaID(inventario.EmpresaID);

                var responsaveis = _responsavelRepository.GetByEmpresaID(inventario.EmpresaID);

                var centrosCusto = _centroCustoRepository.GetByEmpresaID(inventario.EmpresaID);

                var contasContabeis = _contaContabilRepository.GetByEmpresaID(inventario.EmpresaID);

                var filiais = _filialDbRepository.GetByEmpresaID(inventario.EmpresaID);

                var locais = _localDbRepository.GetByEmpresaID(inventario.EmpresaID);

                var configs = _configRepository.GetByInventarioID(inventario.ID);

                var itens = _itemRepository.GetByEmpresaID(inventario.EmpresaID);

                var itemEstados = _itemEstadoRepository.GetAll();

                object carga = new
                {
                    itemEstado = itemEstados.Count == 0 ? null : itemEstados.Select(ie => new
                    {
                        ie.ID,
                        ie.Nome
                    }),

                    empresa = new
                    {
                        empresa.CNPJ,
                        empresa.DataCadastroFormatada,
                        empresa.ID,
                        empresa.NomeFantasia,
                        empresa.RazaoSocial,

                        grupos = grupos.Count == 0 ? null : grupos.Select(g => new
                        {
                            g.Codigo,
                            g.ContaContabilID,
                            g.DataCadastroFormatada,
                            g.ID,
                            g.Nome
                        }),

                        especies = especies.Count == 0 ? null : especies.Select(e => new
                        {
                            e.Codigo,
                            e.DataCadastroFormatada,
                            e.GrupoID,
                            e.ID,
                            e.Nome,
                            propriedades = especiePropriedades.Where(ep => ep.EspecieID == e.ID).Select(ep => new
                            {
                                ep.DataCadastroFormatada,
                                ep.EspecieID,
                                ep.PropriedadeID
                            })
                        }),

                        propriedades = propriedades.Count == 0 ? null : propriedades.Select(p => new
                        {
                            p.Codigo,
                            p.DataCadastroFormatada,
                            p.Fixa,
                            p.ID,
                            p.Nome
                        }),

                        responsaveis = responsaveis.Count == 0 ? null : responsaveis.Select(r => new
                        {
                            r.Cargo,
                            r.DataCadastroFormatada,
                            r.ID,
                            r.Matricula,
                            r.Nome,
                            r.UsuarioID
                        }),

                        centrosCusto = centrosCusto.Count == 0 ? null : centrosCusto.Select(cc => new
                        {
                            cc.Codigo,
                            cc.CodigoInterno,
                            cc.DataCadastroFormatada,
                            cc.ID,
                            cc.Nome,
                            cc.Nivel,
                            cc.PaiID,
                            cc.ResponsavelID
                        }),

                        contasContabeis = contasContabeis.Count == 0 ? null : contasContabeis.Select(cco => new
                        {
                            cco.Codigo,
                            cco.CodigoInterno,
                            cco.DataCadastroFormatada,
                            cco.ID,
                            cco.Nivel,
                            cco.Nome,
                            cco.NomeAbreviado,
                            cco.PaiID
                        })
                    },

                    inventario = new
                    {

                        inventario.ID,
                        inventario.Codigo,
                        inventario.Nome,
                        inventario.StatusID,

                        filiais = filiais.Count == 0 ? null : filiais.Select(f => new
                        {
                            f.ID,
                            f.Codigo,
                            f.CNPJ,
                            f.Nome
                        }),

                        locais = locais.Count == 0 ? null : locais.Select(l => new
                        {
                            l.CentroCustoID,
                            l.Codigo,
                            l.CodigoInterno,
                            l.DataCadastroFormatada,
                            l.FilialID,
                            l.ID,
                            l.Nivel,
                            l.Nome,
                            l.PaiID,
                            l.ResponsavelID

                        }),

                        itens = itens.Count == 0 ? null : itens.Select(i => new
                        {
                            i.Codigo,
                            i.CodigoAnterior,
                            i.CodigoPM,
                            i.DadosPM,
                            i.DataCadastroFormatada,
                            i.EspecieID,
                            i.Local.FilialID,
                            i.ID,
                            i.ImagemUrl,
                            i.Imagens,
                            i.Incorporacao,
                            i.IncorporacaoAnterior,
                            i.ItemEstadoID,
                            i.Latitude,
                            i.LocalID,
                            i.LocalPM,
                            i.Longitude,
                            i.Marca,
                            i.Modelo,
                            i.Nome,
                            i.NumeroSerie,
                            i.Observacao,
                            i.ResponsavelID,
                            i.StatusID,
                            i.StatusNome,
                            i.Tag
                        }),

                        configs = configs.Select(c => new
                        {

                            c.EntidadeCampo.NomeCampo,
                            c.Visivel,
                            c.Obrigatorio
                        })
                    }
                };

                return Ok(carga);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
