using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class EmpresaController : ApiController
    {
        private readonly IEmpresa _repository;
        private readonly IPropriedade _propriedadeRepository;
        private readonly IPropriedadeValor _propriedadeValorRepository;
        private readonly IGrupo _grupoRepository;
        private readonly IContaContabil _contaContabilRepository;
        private readonly ICentroCusto _centroCustoRepository;
        private readonly IEspecie _especieRepository;
        private readonly IEspeciePropriedade _especiePropriedadeRepository;

        public EmpresaController(IEmpresa repository, IPropriedade propriedadeRepository, IPropriedadeValor propriedadeValorRepository, IGrupo grupoRepository, IContaContabil contaContabilRepository, ICentroCusto centroCustoRepository, IEspecie especieRepository, IEspeciePropriedade especiePropriedadeRepository)
        {
            _repository = repository;
            _propriedadeRepository = propriedadeRepository;
            _propriedadeValorRepository = propriedadeValorRepository;
            _grupoRepository = grupoRepository;
            _contaContabilRepository = contaContabilRepository;
            _centroCustoRepository = centroCustoRepository;
            _especieRepository = especieRepository;
            _especiePropriedadeRepository = especiePropriedadeRepository;
        }

        /// <summary>
        /// Retorna Lista de Empresas Ativas Cadastrados
        /// </summary>
        [HttpGet]
        [Route("api/empresa/getempresas")]
        public IHttpActionResult GetEmpresas()
        {
            try
            {
                return Ok(_repository.GetAll());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Lista de Empresas com Inventários Cadastrados de Acordo com o ID do Usuário
        /// </summary>
        /// /// <param name="usuarioID">ID do Usuário</param>
        [HttpGet]
        [Route("api/empresa/getByInventarioUsuarioID/{usuarioID}")]
        public IHttpActionResult GetByInventarioUsuarioID(int usuarioID)
        {
            try
            {
                var empresas = _repository.GetByInventarioUsuarioID(usuarioID);

                return Ok(empresas.Select(x => new { x.ID, x.NomeFantasia }).ToList());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Empresa relacionada ao ID informado
        /// </summary>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var empresa = _repository.GetByID(id);

                if (empresa == null)
                    return NotFound();

                return Ok(empresa);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui uma Empresa
        /// </summary>
        /// <param name="empresa">Objeto do tipo Empresa</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] Empresa empresa)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _repository.Insert(empresa);

                if (empresa.CopiarDe != 0)
                {
                    var deParaPropriedade = new Dictionary<int, int>();
                    var deParaGrupo = new Dictionary<int, int>();
                    var deParaContaContabil = new Dictionary<int, int>();
                    var deParaCentroCusto = new Dictionary<int, int>();
                    var deParaEspecie = new Dictionary<int, int>();

                    if (empresa.CopiarEntidades.Contains("propriedade"))
                    {
                        var propriedadeBase = _propriedadeRepository.GetByEmpresaID(empresa.CopiarDe);

                        foreach (var pb in propriedadeBase)
                        {
                            var propriedadeNova = new Propriedade()
                            {
                                EmpresaID = empresa.ID,
                                Nome = pb.Nome,
                                Fixa = pb.Fixa
                            };

                            _propriedadeRepository.Insert(propriedadeNova);

                            deParaPropriedade.Add(pb.ID, propriedadeNova.ID);

                            if (pb.Fixa)
                            {
                                foreach (var pbValor in pb.Valores)
                                {
                                    _propriedadeValorRepository.Insert(new PropriedadeValor()
                                    {
                                        PropriedadeID = propriedadeNova.ID,
                                        Valor = pbValor.Valor
                                    });
                                }
                            }
                        }
                    }

                    if (empresa.CopiarEntidades.Contains("contaContabil"))
                    {
                        var contaContabilBase = _contaContabilRepository.GetByEmpresaID(empresa.CopiarDe).OrderBy(x => x.CodigoInterno);

                        foreach (var ccb in contaContabilBase)
                        {
                            var contaContabilNova = new ContaContabil()
                            {
                                Codigo = ccb.Codigo,
                                CodigoInterno = ccb.CodigoInterno,
                                EmpresaID = empresa.ID,
                                Nome = ccb.Nome,
                                NomeAbreviado = ccb.NomeAbreviado,
                                PaiID = ccb.PaiID == null ? null : (int?)deParaContaContabil.Where(x => x.Key == ccb.PaiID).Single().Value
                            };

                            _contaContabilRepository.Insert(contaContabilNova);

                            deParaContaContabil.Add(ccb.ID, contaContabilNova.ID);
                        }
                    }

                    if (empresa.CopiarEntidades.Contains("grupo"))
                    {
                        var grupoBase = _grupoRepository.GetByEmpresaID(empresa.CopiarDe);

                        foreach (var gb in grupoBase)
                        {
                            var grupoNovo = new Grupo()
                            {
                                Codigo = gb.Codigo,
                                ContaContabilID = gb.ContaContabilID == null ? null : deParaContaContabil.Count == 0 ? null : (int?)deParaContaContabil.Where(x => x.Key == gb.ContaContabilID).Single().Value,
                                EmpresaID = empresa.ID,
                                Nome = gb.Nome
                            };

                            _grupoRepository.Insert(grupoNovo);

                            deParaGrupo.Add(gb.ID, grupoNovo.ID);
                        }

                        if (empresa.CopiarEntidades.Contains("especie"))
                        {
                            var especieBase = _especieRepository.GetByEmpresaID(empresa.CopiarDe);

                            foreach (var eb in especieBase)
                            {
                                var especieNova = new Especie() {
                                    Codigo = eb.Codigo,
                                    GrupoID = deParaGrupo.Where(x => x.Key == eb.GrupoID).Single().Value,
                                    Nome = eb.Nome
                                };

                                _especieRepository.Insert(especieNova);

                                deParaEspecie.Add(eb.ID, especieNova.ID);
                            }

                            if (deParaPropriedade.Count > 0)
                            {
                                var especiePropriedadeBase = _especiePropriedadeRepository.GetByEmpresaID(empresa.CopiarDe);

                                foreach (var epb in especiePropriedadeBase)
                                {
                                    var especiePropriedadeNova = new EspeciePropriedade() {
                                        EspecieID = deParaEspecie.Where(x => x.Key == epb.EspecieID).Single().Value,
                                        PropriedadeID = deParaPropriedade.Where(x => x.Key == epb.PropriedadeID).Single().Value
                                    };

                                    _especiePropriedadeRepository.Insert(especiePropriedadeNova);
                                }
                            }                            
                        }
                    }

                    if (empresa.CopiarEntidades.Contains("centroCusto"))
                    {
                        var centroCustoBase = _centroCustoRepository.GetByEmpresaID(empresa.CopiarDe).OrderBy(x => x.CodigoInterno);

                        foreach (var ccb in centroCustoBase)
                        {
                            var centroCustoNovo = new CentroCusto()
                            {
                                Codigo = ccb.Codigo,
                                CodigoInterno = ccb.CodigoInterno,
                                EmpresaID = empresa.ID,
                                Nome = ccb.Nome,
                                PaiID = ccb.PaiID == null ? null : (int?)deParaCentroCusto.Where(x => x.Key == ccb.PaiID).Single().Value
                            };

                            _centroCustoRepository.Insert(centroCustoNovo);

                            deParaCentroCusto.Add(ccb.ID, centroCustoNovo.ID);
                        }
                    }
                }

                return Ok(empresa);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de uma EMpresa
        /// </summary>
        /// <param name="empresa">Objeto do tipo Empresa</param>
        /// <param name="id">ID da Empresa que será atualizada</param>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Empresa empresa)
        {
            try
            {
                var empresaDB = _repository.GetByID(id);

                if (empresaDB == null)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                empresaDB.Bairro = empresa.Bairro;
                empresaDB.CEP = empresa.CEP;
                empresaDB.Cidade = empresa.Cidade;
                empresaDB.CNPJ = empresa.CNPJ;
                empresaDB.Complemento = empresa.Complemento;
                empresaDB.Endereco = empresa.Endereco;
                empresaDB.NomeFantasia = empresa.NomeFantasia;
                empresaDB.Numero = empresa.Numero;
                empresaDB.RazaoSocial = empresa.RazaoSocial;
                empresaDB.UF = empresa.UF;
                empresaDB.Logo = empresa.Logo;
                empresaDB.PermiteCodigosIguais = empresa.PermiteCodigosIguais;

                _repository.Update(empresaDB);

                return Ok(empresaDB);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui uma Empresa
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var empresa = _repository.GetByID(id);

                if (empresa == null)
                    return NotFound();

                _repository.Delete(id);

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
