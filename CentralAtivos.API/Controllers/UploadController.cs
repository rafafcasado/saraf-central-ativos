using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using CentralAtivos.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class UploadController : ApiController
    {
        private readonly ICentroCusto _entidadeCentroCustoRepository;
        private readonly IContaContabil _entidadeContaContabilRepository;
        private readonly IEmpresa _entidadeEmpresaRepository;
        private readonly IEntidadeCampo _entidadeCampoRepository;
        private readonly IEspecie _entidadeEspecieRepository;
        private readonly IFilial _entidadeFilialRepository;
        private readonly IGrupo _entidadeGrupoRepository;
        private readonly IItem _entidadeItemRepository;
        private readonly IItemEstado _entidadeItemEstadoRepository;
        private readonly ILocal _entidadeLocalRepository;
        private readonly IPropriedade _entidadePropriedadeRepository;
        private readonly IPropriedadeValor _entidadePropriedadeValorRepository;
        private readonly IResponsavel _entidadeResponsavelRepository;
        private readonly IUpload _repository;


        public UploadController(IUpload repository, IEntidadeCampo entidadeCampoRepository, IEmpresa entidadeEmpresaRepository, IFilial entidadeFilialRepository, IGrupo entidadeGrupoRepository, IPropriedade entidadePropriedadeRepository, IPropriedadeValor entidadePropriedadeValorRepository, IResponsavel entidadeResponsavelRepository, IItemEstado entidadeItemEstadoRepository, ILocal entidadeLocalRepository, IEspecie entidadeEspecieRepository, IItem entidadeItemRepository, IContaContabil entidadeContaContabilRepository, ICentroCusto entidadeCentroCustoRepository)
        {
            _repository = repository;
            _entidadeCampoRepository = entidadeCampoRepository;
            _entidadeEmpresaRepository = entidadeEmpresaRepository;
            _entidadeFilialRepository = entidadeFilialRepository;
            _entidadeGrupoRepository = entidadeGrupoRepository;
            _entidadePropriedadeRepository = entidadePropriedadeRepository;
            _entidadePropriedadeValorRepository = entidadePropriedadeValorRepository;
            _entidadeResponsavelRepository = entidadeResponsavelRepository;
            _entidadeItemEstadoRepository = entidadeItemEstadoRepository;
            _entidadeLocalRepository = entidadeLocalRepository;
            _entidadeEspecieRepository = entidadeEspecieRepository;
            _entidadeItemRepository = entidadeItemRepository;
            _entidadeContaContabilRepository = entidadeContaContabilRepository;
            _entidadeCentroCustoRepository = entidadeCentroCustoRepository;
        }

        /// <summary>
        /// Retorna Lista de Uploads Cadastrados
        /// </summary>
        [HttpGet]
        [Route("api/upload/getUploads")]
        public IHttpActionResult GetUploads()
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
        /// Inclui um Novo Processo de Upload
        /// </summary>
        [HttpPost, Route("api/upload/novo/{empresaID}/{entidadeID}/{regraUpload}")]
        public HttpResponseMessage Novo(int empresaID, int entidadeID, int regraUpload)
        {
            var empresa = _entidadeEmpresaRepository.GetByID(empresaID);

            if (empresa == null)
                Request.CreateResponse(HttpStatusCode.BadRequest, "Empresa não localizada");

            if (Enum.TryParse(entidadeID.ToString(), out Enums.Entidade entidade))
                if (!Enum.IsDefined(typeof(Enums.Entidade), entidade))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Entidade não localizada");

            HttpResponseMessage result = null;

            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count > 0)
            {
                var postedFile = httpRequest.Files[0];

                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

                var upload = new Upload()
                {
                    EmpresaID = empresaID,
                    Entidade = (Enums.Entidade)entidadeID,
                    NomeArquivo = postedFile.FileName,
                    Status = Enums.StatusUpload.EM_FILA_DE_PROCESSAMENTO,
                    UsuarioID = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == "UsuarioID").Value),
                    RegraUploadID = regraUpload
                };

                _repository.Insert(upload);

                var filePath = System.Configuration.ConfigurationManager.AppSettings["Uploads"] + upload.ID + "\\";

                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                filePath += upload.NomeArquivo;

                postedFile.SaveAs(filePath);

                result = Request.CreateResponse(HttpStatusCode.OK, upload);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return result;
        }

        /// <summary>
        /// Inicia Processamento de Arquivo
        /// </summary>
        [HttpPost, Route("api/upload/processar/{id}")]
        public HttpResponseMessage Processar(int id)
        {
            var upload = _repository.GetByID(id);

            if (upload == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Upload não Localizado");

            upload.Status = Enums.StatusUpload.PROCESSANDO;
            _repository.Update(upload);

            try
            {
                var filePath = System.Configuration.ConfigurationManager.AppSettings["Uploads"] + upload.ID + "\\" + upload.NomeArquivo;

                DataTable dtExcel = Excel.CSVToDataTable(filePath);

                var matrizItens = _entidadeCampoRepository.GetByEntidadeID((int)upload.Entidade);

                if (matrizItens.Count == 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Matriz de Campos não cadastrada para essa Entidade");

                var colunasOK = true;

                foreach (var item in matrizItens)
                {
                    if (!dtExcel.Columns.Contains(item.NomeCampo))
                        colunasOK = false;
                }

                if (!colunasOK)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Estrutura de Colunas fora do padrão dessa Entidade");

                DataTable dadosOK = new DataTable();
                DataTable dadosInvalidos = new DataTable();

                foreach (var col in dtExcel.Columns)
                {
                    dadosOK.Columns.Add(new DataColumn(col.ToString()));
                    dadosInvalidos.Columns.Add(new DataColumn(col.ToString()));
                }

                dadosInvalidos.Columns.Add(new DataColumn("Critica"));

                foreach (DataRow dr in dtExcel.Rows)
                {
                    var linhaOK = true;
                    var critica = string.Empty;

                    foreach (var item in matrizItens)
                    {
                        var valor = dr[item.NomeCampo];

                        if (item.Obrigatorio && string.IsNullOrEmpty(valor.ToString()))
                        {
                            linhaOK = false;
                            critica += $"O Campo {item.NomeCampo} é obrigatório e não foi preenchido; ";
                        }

                        if (item.Tipo == Enums.TipoItem.DECIMAL && !string.IsNullOrEmpty(valor.ToString()))
                            if (!decimal.TryParse(valor.ToString(), out decimal valorDecimal))
                            {
                                linhaOK = false;
                                critica += $"O Campo {item.NomeCampo} é do tipo {Enums.TipoItem.DECIMAL} e houve erro na conversão; ";
                            }

                        if (item.Tipo == Enums.TipoItem.INT && !string.IsNullOrEmpty(valor.ToString()))
                            if (!int.TryParse(valor.ToString(), out int valorInt))
                            {
                                linhaOK = false;
                                critica += $"O Campo {item.NomeCampo} é do tipo {Enums.TipoItem.INT} e houve erro na conversão; ";
                            }

                        if (item.Tipo == Enums.TipoItem.BOOLEAN && !string.IsNullOrEmpty(valor.ToString()))
                            if (!bool.TryParse(valor.ToString(), out bool valorBool))
                            {
                                linhaOK = false;
                                critica += $"O Campo {item.NomeCampo} é do tipo {Enums.TipoItem.BOOLEAN} e houve erro na conversão; ";
                            }

                        if (item.Tipo == Enums.TipoItem.DATETIME && !string.IsNullOrEmpty(valor.ToString()))
                            if (!DateTime.TryParse(valor.ToString(), out DateTime valorDateTime))
                            {
                                linhaOK = false;
                                critica += $"O Campo {item.NomeCampo} é do tipo {Enums.TipoItem.DATETIME} e houve erro na conversão; ";
                            }

                        if (item.Tipo == Enums.TipoItem.VARCHAR && !string.IsNullOrEmpty(item.Regex))
                        {
                            Regex regex = new Regex(item.Regex);

                            Match match = regex.Match(valor.ToString());

                            if (!match.Success)
                            {
                                linhaOK = false;
                                critica += $"O Campo {item.NomeCampo} não está em um formato correto; ";
                            }
                        }

                        if (item.Tipo == Enums.TipoItem.VARCHAR && !string.IsNullOrEmpty(item.Tamanho.ToString()))
                            if (valor.ToString().Length > item.Tamanho)
                            {
                                linhaOK = false;
                                critica += $"O Campo {item.NomeCampo} tem um limite de {item.Tamanho} caracteres e esse limite foi ultrapassado; ";
                            }
                    }

                    if (linhaOK)
                    {
                        DataRow rowOK;

                        rowOK = dadosOK.NewRow();

                        foreach (var col in dtExcel.Columns)
                        {
                            rowOK[col.ToString()] = dr[col.ToString()];
                        }

                        dadosOK.Rows.Add(rowOK);
                    }
                    else
                    {
                        DataRow rowInvalido;

                        rowInvalido = dadosInvalidos.NewRow();

                        foreach (var col in dtExcel.Columns)
                        {
                            rowInvalido[col.ToString()] = dr[col.ToString()];
                        }

                        rowInvalido["Critica"] = critica;

                        dadosInvalidos.Rows.Add(rowInvalido);
                    }

                }

                // UPLOAD DE FILIAIS
                if (upload.Entidade == Enums.Entidade.FILIAL)
                {
                    var filiaisCadastradas = _entidadeFilialRepository.GetByEmpresaID(upload.EmpresaID);
                    
                    if (upload.RegraUploadID == 3)
                        _entidadeFilialRepository.DeleteAll(upload.EmpresaID);

                    foreach (DataRow dr in dadosOK.Rows)
                    {
                        var filial = new Filial()
                        {
                            EmpresaID = upload.EmpresaID,
                            Codigo = dr["Codigo"].ToString(),
                            Nome = dr["Nome"].ToString(),
                            Bairro = dr["Bairro"].ToString(),
                            CEP = dr["CEP"].ToString(),
                            Cidade = dr["Cidade"].ToString(),
                            CNPJ = dr["CNPJ"].ToString(),
                            Complemento = dr["Complemento"].ToString(),
                            Endereco = dr["Endereco"].ToString(),
                            Numero = int.Parse(dr["Numero"].ToString()),
                            UF = dr["UF"].ToString()
                        };

                        if (upload.RegraUploadID == 3)
                            _entidadeFilialRepository.Insert(filial);
                        else
                        {
                            var filialAtualizar = filiaisCadastradas.Where(x => x.Codigo == filial.Codigo).SingleOrDefault();

                            if (filialAtualizar == null)
                                _entidadeFilialRepository.Insert(filial);
                            else
                            {
                                if (upload.RegraUploadID == 1)
                                {
                                    filialAtualizar.Nome = filial.Nome;
                                    filialAtualizar.Bairro = filial.Bairro;
                                    filialAtualizar.CEP = filial.CEP;
                                    filialAtualizar.Cidade = filial.Cidade;
                                    filialAtualizar.CNPJ = filial.CNPJ;
                                    filialAtualizar.Complemento = filial.Complemento;
                                    filialAtualizar.Endereco = filial.Endereco;
                                    filialAtualizar.Numero = filial.Numero;
                                    filialAtualizar.UF = filial.UF;

                                    _entidadeFilialRepository.Update(filialAtualizar);
                                }
                            }
                        }
                    }
                }

                // UPLOAD DE GRUPOS
                if (upload.Entidade == Enums.Entidade.GRUPO)
                {
                    var gruposCadastradados = _entidadeGrupoRepository.GetByEmpresaID(upload.EmpresaID);

                    if (upload.RegraUploadID == 3)
                        _entidadeGrupoRepository.DeleteAll(upload.EmpresaID);

                    List<ContaContabil> contasContabeis = _entidadeContaContabilRepository.GetByEmpresaID(upload.EmpresaID);

                    dadosOK.Columns.Add(new DataColumn("ContaContabilID"));
                    dadosOK.Columns.Add(new DataColumn("Critica"));

                    foreach (DataRow dr in dadosOK.Rows)
                    {
                        string critica = string.Empty;

                        if (!string.IsNullOrEmpty(dr["ContaContabilCodigo"].ToString()))
                        {

                            var contaContabil = contasContabeis.Where(x => x.Codigo == dr["ContaContabilCodigo"].ToString()).SingleOrDefault();

                            if (contaContabil == null)
                                critica += "Conta Contábil não cadastrada";
                            else
                                dr["ContaContabilID"] = contaContabil.ID;

                        }

                        if (!string.IsNullOrEmpty(critica))
                        {
                            DataRow rowInvalido;

                            rowInvalido = dadosInvalidos.NewRow();

                            rowInvalido["ContaContabilCodigo"] = dr["ContaContabilCodigo"];
                            rowInvalido["Codigo"] = dr["Codigo"];
                            rowInvalido["Nome"] = dr["Nome"];
                            rowInvalido["Critica"] = critica;

                            dadosInvalidos.Rows.Add(rowInvalido);
                        }
                        else
                        {
                            var grupo = new Grupo()
                            {
                                Codigo = dr["Codigo"].ToString(),
                                ContaContabilID = string.IsNullOrEmpty(dr["ContaContabilID"].ToString()) ? (int?)null : Convert.ToInt32(dr["ContaContabilID"].ToString()),
                                EmpresaID = upload.EmpresaID,
                                Nome = dr["Nome"].ToString()
                            };

                            if (upload.RegraUploadID == 3)
                                _entidadeGrupoRepository.Insert(grupo);
                            else
                            {
                                var grupoAtualizar = gruposCadastradados.Where(x => x.Codigo == grupo.Codigo).SingleOrDefault();

                                if (grupoAtualizar == null)
                                    _entidadeGrupoRepository.Insert(grupo);
                                else
                                {
                                    if (upload.RegraUploadID == 1)
                                    {
                                        grupoAtualizar.ContaContabilID = grupo.ContaContabilID;
                                        grupoAtualizar.Nome = grupo.Nome;

                                        _entidadeGrupoRepository.Update(grupoAtualizar);
                                    }
                                }
                            }
                        }
                    }
                }

                // UPLOAD DE PROPRIEDADES
                if (upload.Entidade == Enums.Entidade.PROPRIEDADE)
                {
                    var propriedadesCadastradas = _entidadePropriedadeRepository.GetByEmpresaID(upload.EmpresaID);

                    if (upload.RegraUploadID == 3)
                        _entidadePropriedadeRepository.DeleteAll(upload.EmpresaID);

                    foreach (DataRow dr in dadosOK.Rows)
                    {
                        var prop = new Propriedade()
                        {
                            Codigo = dr["Codigo"].ToString(),
                            EmpresaID = upload.EmpresaID,
                            Fixa = Convert.ToBoolean(dr["Fixa"].ToString()),
                            Nome = dr["Nome"].ToString()
                        };

                        if (upload.RegraUploadID == 3)
                        {
                            _entidadePropriedadeRepository.Insert(prop);

                            if (!string.IsNullOrEmpty(dr["Valores"].ToString()))
                            {
                                var valores = dr["Valores"].ToString().Split(',');

                                foreach (var valor in valores)
                                {
                                    _entidadePropriedadeValorRepository.Insert(new PropriedadeValor()
                                    {
                                        PropriedadeID = prop.ID,
                                        Valor = valor
                                    });
                                }
                            }
                        }                            
                        else
                        {
                            var propriedadeAtualizar = propriedadesCadastradas.Where(x => x.Codigo == prop.Codigo).SingleOrDefault();

                            if (propriedadeAtualizar == null)
                            {
                                _entidadePropriedadeRepository.Insert(prop);

                                if (!string.IsNullOrEmpty(dr["Valores"].ToString()))
                                {
                                    var valores = dr["Valores"].ToString().Split(',');

                                    foreach (var valor in valores)
                                    {
                                        _entidadePropriedadeValorRepository.Insert(new PropriedadeValor()
                                        {
                                            PropriedadeID = prop.ID,
                                            Valor = valor
                                        });
                                    }
                                }
                            }                                
                            else
                            {
                                if (upload.RegraUploadID == 1)
                                {
                                    propriedadeAtualizar.Nome = prop.Nome;
                                    propriedadeAtualizar.Fixa = prop.Fixa;

                                    _entidadePropriedadeRepository.Update(propriedadeAtualizar);

                                    if (!string.IsNullOrEmpty(dr["Valores"].ToString()))
                                    {
                                        var valores = dr["Valores"].ToString().Split(',');

                                        var valoresExcluir = propriedadeAtualizar.Valores.Select(x => x.Valor).Except(valores);

                                        if (valoresExcluir.Count() > 0)
                                        {
                                            foreach (var item in valoresExcluir)
                                            {
                                                _entidadePropriedadeValorRepository.Delete(propriedadeAtualizar.Valores.Where(x => x.Valor == item).Single().ID);
                                            }
                                        }

                                        var valoresIncluir = valores.Except(propriedadeAtualizar.Valores.Select(x => x.Valor));

                                        foreach (var item in valoresIncluir)
                                        {
                                            _entidadePropriedadeValorRepository.Insert(new PropriedadeValor()
                                            {
                                                PropriedadeID = propriedadeAtualizar.ID,
                                                Valor = item
                                            });
                                        }
                                    }
                                }
                            }
                        }                        
                    }
                }

                // UPLOAD DE RESPONSÁVEIS
                if (upload.Entidade == Enums.Entidade.RESPONSAVEL)
                {
                    var responsaveisCadastrados = _entidadeResponsavelRepository.GetByEmpresaID(upload.EmpresaID);

                    if (upload.RegraUploadID == 3)
                        _entidadeResponsavelRepository.DeleteAll(upload.EmpresaID);

                    foreach (DataRow dr in dadosOK.Rows)
                    {
                        var responsavel = new Responsavel()
                        {
                            Cargo = dr["Cargo"].ToString(),
                            EmpresaID = upload.EmpresaID,
                            Matricula = dr["Matricula"].ToString(),
                            Nome = dr["Nome"].ToString()
                        };

                        if (upload.RegraUploadID == 3)
                            _entidadeResponsavelRepository.Insert(responsavel);
                        else
                        {
                            var responsavelAtualizar = responsaveisCadastrados.Where(x => x.Nome == responsavel.Nome && x.Matricula == responsavel.Matricula).SingleOrDefault();

                            if (responsavelAtualizar == null)
                                _entidadeResponsavelRepository.Insert(responsavel);
                            else
                            {
                                if (upload.RegraUploadID == 1)
                                {
                                    responsavelAtualizar.Cargo = responsavel.Cargo;
                                    responsavelAtualizar.Matricula = responsavel.Matricula;
                                    responsavelAtualizar.Nome = responsavel.Nome;

                                    _entidadeResponsavelRepository.Update(responsavelAtualizar);
                                }
                            }
                        }
                    }
                }

                // UPLOAD DE ITENS
                if (upload.Entidade == Enums.Entidade.ITEM)
                {
                    if (upload.RegraUploadID == 3)
                        _entidadeItemRepository.DeleteAll(upload.EmpresaID);

                    var itensCadastrados = _entidadeItemRepository.GetByEmpresaID(upload.EmpresaID);

                    List<ItemEstado> itensEstado = _entidadeItemEstadoRepository.GetAll();
                    List<Local> locais = _entidadeLocalRepository.GetByEmpresaID(upload.EmpresaID);
                    List<Especie> especies = _entidadeEspecieRepository.GetByEmpresaID(upload.EmpresaID);

                    dadosOK.Columns.Add(new DataColumn("ItemEstadoID"));
                    dadosOK.Columns.Add(new DataColumn("LocalID"));
                    dadosOK.Columns.Add(new DataColumn("EspecieID"));
                    dadosOK.Columns.Add(new DataColumn("Critica"));

                    foreach (DataRow dr in dadosOK.Rows)
                    {
                        string critica = string.Empty;

                        if (!string.IsNullOrEmpty(dr["ItemEstado"].ToString()))
                        {
                            var itemEstadoFormatado = dr["ItemEstado"].ToString().Trim().ToLower();

                            var itemEstado = itensEstado.Where(x => x.Nome.ToLower() == itemEstadoFormatado).SingleOrDefault();

                            if (itemEstado == null)
                                critica += "ItemEstado não cadastrado; ";
                            else
                                dr["ItemEstadoID"] = itemEstado.ID;
                        }

                        var local = locais.Where(x => x.Codigo == dr["LocalCodigo"].ToString()).SingleOrDefault();

                        if (local == null)
                            critica += "Local não cadastrado; ";
                        else
                            dr["LocalID"] = local.ID;

                        var especie = especies.Where(x => x.Codigo == dr["EspecieCodigo"].ToString()).SingleOrDefault();

                        if (especie == null)
                            critica += "Espécie não cadastrada";
                        else
                            dr["EspecieID"] = especie.ID;

                        if (!string.IsNullOrEmpty(critica))
                        {
                            DataRow rowInvalido;

                            rowInvalido = dadosInvalidos.NewRow();

                            rowInvalido["ItemEstado"] = dr["ItemEstado"];
                            rowInvalido["LocalCodigo"] = dr["LocalCodigo"];
                            rowInvalido["EspecieCodigo"] = dr["EspecieCodigo"];
                            rowInvalido["Nome"] = dr["Nome"];
                            rowInvalido["Codigo"] = dr["Codigo"];
                            rowInvalido["Incorporacao"] = dr["Incorporacao"];
                            rowInvalido["CodigoAnterior"] = dr["CodigoAnterior"];
                            rowInvalido["IncorporacaoAnterior"] = dr["IncorporacaoAnterior"];
                            rowInvalido["CodigoPM"] = dr["CodigoPM"];
                            rowInvalido["DadosPM"] = dr["DadosPM"];
                            rowInvalido["LocalPM"] = dr["LocalPM"];
                            rowInvalido["Tag"] = dr["Tag"];
                            rowInvalido["Marca"] = dr["Marca"];
                            rowInvalido["Modelo"] = dr["Modelo"];
                            rowInvalido["NumeroSerie"] = dr["NumeroSerie"];
                            rowInvalido["Observacao"] = dr["Observacao"];
                            rowInvalido["Latitude"] = dr["Latitude"];
                            rowInvalido["Longitude"] = dr["Longitude"];
                            rowInvalido["Critica"] = critica;

                            dadosInvalidos.Rows.Add(rowInvalido);
                        }
                        else
                        {
                            var item = new Item()
                            {
                                Codigo = dr["Codigo"].ToString(),
                                CodigoAnterior = dr["CodigoAnterior"].ToString(),
                                CodigoPM = dr["CodigoPM"].ToString(),
                                DadosPM = dr["DadosPM"].ToString(),
                                EmpresaID = upload.EmpresaID,
                                EspecieID = int.Parse(dr["EspecieID"].ToString()),
                                Incorporacao = string.IsNullOrEmpty(dr["Incorporacao"].ToString()) ? (int?)null : Convert.ToInt32(dr["Incorporacao"].ToString()),
                                IncorporacaoAnterior = string.IsNullOrEmpty(dr["IncorporacaoAnterior"].ToString()) ? (int?)null : Convert.ToInt32(dr["IncorporacaoAnterior"].ToString()),
                                ItemEstadoID = string.IsNullOrEmpty(dr["ItemEstadoID"].ToString()) ? (int?)null : Convert.ToInt32(dr["ItemEstadoID"].ToString()),
                                Latitude = string.IsNullOrEmpty(dr["Latitude"].ToString()) ? (decimal?)null : Convert.ToDecimal(dr["Latitude"].ToString()),
                                LocalID = int.Parse(dr["LocalID"].ToString()),
                                LocalPM = dr["LocalPM"].ToString(),
                                Longitude = string.IsNullOrEmpty(dr["Longitude"].ToString()) ? (decimal?)null : Convert.ToDecimal(dr["Longitude"].ToString()),
                                Marca = dr["Marca"].ToString(),
                                Modelo = dr["Modelo"].ToString(),
                                Nome = dr["Nome"].ToString(),
                                NumeroSerie = dr["NumeroSerie"].ToString(),
                                Observacao = dr["Observacao"].ToString(),
                                Tag = dr["Tag"].ToString()
                            };

                            if (upload.RegraUploadID == 3)
                                _entidadeItemRepository.Insert(item);
                            else
                            {
                                var itemAtualizar = itensCadastrados.Where(x => x.Codigo == especie.Codigo).SingleOrDefault();

                                if (itemAtualizar == null)
                                    _entidadeItemRepository.Insert(item);
                                else
                                {
                                    if (upload.RegraUploadID == 1)
                                    {
                                        itemAtualizar.Codigo = item.Codigo;
                                        itemAtualizar.CodigoAnterior = item.CodigoAnterior;
                                        itemAtualizar.CodigoPM = item.CodigoPM;
                                        itemAtualizar.DadosPM = item.DadosPM;
                                        itemAtualizar.EmpresaID = item.EmpresaID;
                                        itemAtualizar.EspecieID = item.EspecieID;
                                        itemAtualizar.Incorporacao = item.Incorporacao;
                                        itemAtualizar.IncorporacaoAnterior = item.IncorporacaoAnterior;
                                        itemAtualizar.ItemEstadoID = item.ItemEstadoID;
                                        itemAtualizar.Latitude = item.Latitude;
                                        itemAtualizar.LocalID = item.LocalID;
                                        itemAtualizar.LocalPM = item.LocalPM;
                                        itemAtualizar.Longitude = item.Longitude;
                                        itemAtualizar.Marca = item.Marca;
                                        itemAtualizar.Modelo = item.Modelo;
                                        itemAtualizar.Nome = item.Nome;
                                        itemAtualizar.NumeroSerie = item.NumeroSerie;
                                        itemAtualizar.Observacao = item.Observacao;
                                        itemAtualizar.Tag = item.Tag;

                                        _entidadeItemRepository.Update(itemAtualizar);
                                    }
                                }
                            }
                        }
                    }
                }

                // UPLOAD DE ESPÉCIES
                if (upload.Entidade == Enums.Entidade.ESPECIE)
                {
                    if (upload.RegraUploadID == 3)
                        _entidadeEspecieRepository.DeleteAll(upload.EmpresaID);

                    var especiesCadastradas = _entidadeEspecieRepository.GetByEmpresaID(upload.EmpresaID);

                    List<Grupo> grupos = _entidadeGrupoRepository.GetByEmpresaID(upload.EmpresaID);

                    dadosOK.Columns.Add(new DataColumn("GrupoID"));
                    dadosOK.Columns.Add(new DataColumn("Critica"));

                    foreach (DataRow dr in dadosOK.Rows)
                    {
                        string critica = string.Empty;

                        var grupo = grupos.Where(x => x.Codigo == dr["GrupoCodigo"].ToString()).SingleOrDefault();

                        if (grupo == null)
                            critica += "Grupo não cadastrado";
                        else
                            dr["GrupoID"] = grupo.ID;

                        if (!string.IsNullOrEmpty(critica))
                        {
                            DataRow rowInvalido;

                            rowInvalido = dadosInvalidos.NewRow();

                            rowInvalido["GrupoCodigo"] = dr["GrupoCodigo"];
                            rowInvalido["Codigo"] = dr["Codigo"];
                            rowInvalido["Nome"] = dr["Nome"];
                            rowInvalido["Critica"] = critica;

                            dadosInvalidos.Rows.Add(rowInvalido);
                        }
                        else
                        {
                            var especie = new Especie()
                            {
                                Codigo = dr["Codigo"].ToString(),
                                GrupoID = int.Parse(dr["GrupoID"].ToString()),
                                Nome = dr["Nome"].ToString()
                            };

                            if (upload.RegraUploadID == 3)
                                _entidadeEspecieRepository.Insert(especie);
                            else
                            {
                                var especieAtualizar = especiesCadastradas.Where(x => x.Codigo == especie.Codigo).SingleOrDefault();

                                if (especieAtualizar == null)
                                    _entidadeEspecieRepository.Insert(especie);
                                else
                                {
                                    if (upload.RegraUploadID == 1)
                                    {
                                        especieAtualizar.GrupoID = especie.GrupoID;
                                        especieAtualizar.Nome = especie.Nome;

                                        _entidadeEspecieRepository.Update(especieAtualizar);
                                    }
                                }
                            }
                        }
                    }
                }

                // UPLOAD DE LOCAIS
                if (upload.Entidade == Enums.Entidade.LOCAL)
                {
                    if (upload.RegraUploadID == 3)
                        _entidadeLocalRepository.DeleteAll(upload.EmpresaID);

                    var locaisCadastrados = _entidadeLocalRepository.GetByEmpresaID(upload.EmpresaID);

                    List<CentroCusto> centrosCusto = _entidadeCentroCustoRepository.GetByEmpresaID(upload.EmpresaID);
                    List<Filial> filiais = _entidadeFilialRepository.GetByEmpresaID(upload.EmpresaID);
                    List<Local> locais = _entidadeLocalRepository.GetByEmpresaID(upload.EmpresaID);

                    // Passo 1. Criar Colunas para Armazenar os IDs dos Campos enviados com Código
                    dadosOK.Columns.Add(new DataColumn("CentroCustoID"));
                    dadosOK.Columns.Add(new DataColumn("FilialID"));
                    dadosOK.Columns.Add(new DataColumn("PaiID"));
                    dadosOK.Columns.Add(new DataColumn("ID"));
                    dadosOK.Columns.Add(new DataColumn("Critica"));

                    // Passo 2. Percorrer todo o DataTable verificando se os Códigos de Centros de Custo e Filiais possuem relação no Banco de Dados
                    //          Verifica também se o CodigoPai tem referência no Banco de Dados ou nesse próprio Data Table
                    //          Caso não haja referência, o registro é criticado e enviado para o DataTable de dados inválidos
                    foreach (DataRow dr in dadosOK.Rows)
                    {
                        string critica = string.Empty;

                        if (!string.IsNullOrEmpty(dr["CodigoPai"].ToString()))
                        {
                            if (dr["CodigoPai"].ToString() == dr["Codigo"].ToString())
                                critica += "CodigoPai e Codigo estão com o mesmo valor; ";
                            {
                                var local = locais.Where(x => x.Codigo == dr["CodigoPai"].ToString()).SingleOrDefault();

                                if (local == null)
                                {
                                    if (!dadosOK.Select($"[Codigo] = '{dr["CodigoPai"].ToString()}'").Any())
                                        critica += "CodigoPai sem referência na Base de Dados e nem no Arquivo de Upload; ";
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(dr["CentroCustoCodigo"].ToString()))
                        {
                            var centroCusto = centrosCusto.Where(x => x.Codigo == dr["CentroCustoCodigo"].ToString()).SingleOrDefault();

                            if (centroCusto == null)
                                critica += "Centro de Custo não cadastrado; ";
                            else
                                dr["CentroCustoID"] = centroCusto.ID;
                        }

                        var filial = filiais.Where(x => x.Codigo == dr["FilialCodigo"].ToString()).SingleOrDefault();

                        if (filial == null)
                            critica += "Filial não cadastrada";
                        else
                            dr["FilialID"] = filial.ID;

                        if (!string.IsNullOrEmpty(critica))
                        {
                            dr["Critica"] = critica;

                            DataRow rowInvalido;

                            rowInvalido = dadosInvalidos.NewRow();

                            rowInvalido["CentroCustoCodigo"] = dr["CentroCustoCodigo"];
                            rowInvalido["FilialCodigo"] = dr["FilialCodigo"];
                            rowInvalido["Codigo"] = dr["Codigo"];
                            rowInvalido["Nome"] = dr["Nome"];
                            rowInvalido["CodigoPai"] = dr["CodigoPai"];
                            rowInvalido["Critica"] = critica;

                            dadosInvalidos.Rows.Add(rowInvalido);
                        }
                    }

                    // Passo 3. Filtro o Data Table para trabalhar somente com os dados sem crítica
                    DataRow[] rowsNaoCriticados = dadosOK.Select("LEN([Critica]) IS NULL");

                    if (rowsNaoCriticados.Any())
                    {
                        DataTable dtBase = rowsNaoCriticados.CopyToDataTable();

                        while (dtBase.Select("LEN([ID]) IS NULL").Any())
                        {
                            foreach (DataRow dr in dtBase.Rows)
                            {
                                if (string.IsNullOrEmpty(dr["ID"].ToString()))
                                {
                                    if (string.IsNullOrEmpty(dr["CodigoPai"].ToString()))
                                    {
                                        var local = new Local()
                                        {
                                            CentroCustoID = string.IsNullOrEmpty(dr["CentroCustoID"].ToString()) ? (int?)null : Convert.ToInt32(dr["CentroCustoID"].ToString()),
                                            Codigo = dr["Codigo"].ToString(),
                                            CodigoInterno = locais.Where(x => x.FilialID == int.Parse(dr["FilialID"].ToString()) && x.PaiID == null).Count() == 0 ? 1 : locais.Where(x => x.FilialID == int.Parse(dr["FilialID"].ToString()) && x.PaiID == null).Max(x => x.CodigoInterno) + 1,
                                            FilialID = int.Parse(dr["FilialID"].ToString()),
                                            Nome = dr["Nome"].ToString()
                                        };

                                        if (upload.RegraUploadID == 3)
                                            _entidadeLocalRepository.Insert(local);
                                        else
                                        {
                                            var localAtualizar = locaisCadastrados.Where(x => x.Codigo == local.Codigo).SingleOrDefault();

                                            if (localAtualizar == null)
                                                _entidadeLocalRepository.Insert(local);
                                            else
                                            {
                                                if (upload.RegraUploadID == 1)
                                                {
                                                    localAtualizar.CentroCustoID = local.CentroCustoID;
                                                    localAtualizar.FilialID = local.FilialID;
                                                    localAtualizar.Nome = local.Nome;

                                                    _entidadeLocalRepository.Update(localAtualizar);
                                                }
                                            }
                                        }

                                        dr["ID"] = local.ID;

                                        locais = _entidadeLocalRepository.GetByEmpresaID(upload.EmpresaID);
                                    }
                                    else
                                    {
                                        var pai = locais.Where(x => x.Codigo == dr["CodigoPai"].ToString()).SingleOrDefault();

                                        if (pai != null)
                                        {
                                            double proximoCodigo = 0;

                                            if (pai.Filhos.Count == 0)
                                                proximoCodigo = Convert.ToDouble(pai.CodigoInterno.ToString().Split(',').Count() == 1 ? pai.CodigoInterno.ToString() + "," + "1" : pai.CodigoInterno.ToString().Split(',')[0] + "," + pai.CodigoInterno.ToString().Split(',')[1] + "1");
                                            else
                                            {
                                                double ultimoCodigo = pai.Filhos.Max(x => x.CodigoInterno);
                                                string codigo = (Convert.ToInt32(ultimoCodigo.ToString().Split(',')[1]) + 1).ToString();
                                                proximoCodigo = Convert.ToDouble(ultimoCodigo.ToString().Split(',')[0] + "," + codigo);
                                            }

                                            var local = new Local()
                                            {
                                                CentroCustoID = string.IsNullOrEmpty(dr["CentroCustoID"].ToString()) ? (int?)null : Convert.ToInt32(dr["CentroCustoID"].ToString()),
                                                Codigo = dr["Codigo"].ToString(),
                                                CodigoInterno = proximoCodigo,
                                                FilialID = int.Parse(dr["FilialID"].ToString()),
                                                Nome = dr["Nome"].ToString(),
                                                PaiID = pai.ID
                                            };

                                            if (upload.RegraUploadID == 3)
                                                _entidadeLocalRepository.Insert(local);
                                            else
                                            {
                                                var localAtualizar = locaisCadastrados.Where(x => x.Codigo == local.Codigo).SingleOrDefault();

                                                if (localAtualizar == null)
                                                    _entidadeLocalRepository.Insert(local);
                                                else
                                                {
                                                    if (upload.RegraUploadID == 1)
                                                    {
                                                        localAtualizar.CentroCustoID = local.CentroCustoID;
                                                        localAtualizar.FilialID = local.FilialID;
                                                        localAtualizar.Nome = local.Nome;

                                                        _entidadeLocalRepository.Update(localAtualizar);
                                                    }
                                                }
                                            }

                                            dr["ID"] = local.ID;

                                            locais = _entidadeLocalRepository.GetByEmpresaID(upload.EmpresaID);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // UPLOAD DE CENTROS DE CUSTO
                if (upload.Entidade == Enums.Entidade.CENTRO_CUSTO)
                {
                    if (upload.RegraUploadID == 3)
                        _entidadeCentroCustoRepository.DeleteAll(upload.EmpresaID);

                    List<CentroCusto> centrosCusto = _entidadeCentroCustoRepository.GetByEmpresaID(upload.EmpresaID);

                    dadosOK.Columns.Add(new DataColumn("PaiID"));
                    dadosOK.Columns.Add(new DataColumn("ID"));
                    dadosOK.Columns.Add(new DataColumn("Critica"));

                    foreach (DataRow dr in dadosOK.Rows)
                    {
                        string critica = string.Empty;

                        if (!string.IsNullOrEmpty(dr["CodigoPai"].ToString()))
                        {
                            if (dr["CodigoPai"].ToString() == dr["Codigo"].ToString())
                                critica += "CodigoPai e Codigo estão com o mesmo valor; ";
                            else
                            {
                                var centroCusto = centrosCusto.Where(x => x.Codigo == dr["CodigoPai"].ToString()).SingleOrDefault();

                                if (centroCusto == null)
                                {
                                    if (!dadosOK.Select($"[Codigo] = '{dr["CodigoPai"].ToString()}'").Any())
                                        critica += "CodigoPai sem referência na Base de Dados e nem no Arquivo de Upload; ";
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(critica))
                        {
                            dr["Critica"] = critica;

                            DataRow rowInvalido;

                            rowInvalido = dadosInvalidos.NewRow();

                            rowInvalido["Codigo"] = dr["Codigo"];
                            rowInvalido["Nome"] = dr["Nome"];
                            rowInvalido["CodigoPai"] = dr["CodigoPai"];
                            rowInvalido["Critica"] = critica;

                            dadosInvalidos.Rows.Add(rowInvalido);
                        }
                    }

                    DataRow[] rowsNaoCriticados = dadosOK.Select("LEN([Critica]) IS NULL");

                    if (rowsNaoCriticados.Any())
                    {
                        DataTable dtBase = rowsNaoCriticados.CopyToDataTable();

                        while (dtBase.Select("LEN([ID]) IS NULL").Any())
                        {
                            foreach (DataRow dr in dtBase.Rows)
                            {
                                if (string.IsNullOrEmpty(dr["ID"].ToString()))
                                {
                                    if (string.IsNullOrEmpty(dr["CodigoPai"].ToString()))
                                    {
                                        var centroCusto = new CentroCusto()
                                        {
                                            EmpresaID = upload.EmpresaID,
                                            Codigo = dr["Codigo"].ToString(),
                                            CodigoInterno = centrosCusto.Where(x => x.PaiID == null).Count() == 0 ? 1 : centrosCusto.Where(x => x.PaiID == null).Max(x => x.CodigoInterno) + 1,
                                            Nome = dr["Nome"].ToString()
                                        };

                                        if (upload.RegraUploadID == 3)
                                            _entidadeCentroCustoRepository.Insert(centroCusto);
                                        else
                                        {
                                            var centroCustoAtualizar = centrosCusto.Where(x => x.Codigo == centroCusto.Codigo).SingleOrDefault();

                                            if (centroCustoAtualizar == null)
                                                _entidadeCentroCustoRepository.Insert(centroCusto);
                                            else
                                            {
                                                if (upload.RegraUploadID == 1)
                                                {
                                                    centroCustoAtualizar.Nome = centroCusto.Nome;

                                                    _entidadeCentroCustoRepository.Update(centroCustoAtualizar);
                                                }
                                            }
                                        }

                                        dr["ID"] = centroCusto.ID;

                                        centrosCusto = _entidadeCentroCustoRepository.GetByEmpresaID(upload.EmpresaID);
                                    }
                                    else
                                    {
                                        var pai = centrosCusto.Where(x => x.Codigo == dr["CodigoPai"].ToString()).SingleOrDefault();

                                        if (pai != null)
                                        {
                                            double proximoCodigo = 0;

                                            if (pai.Filhos.Count == 0)
                                                proximoCodigo = Convert.ToDouble(pai.CodigoInterno.ToString().Split(',').Count() == 1 ? pai.CodigoInterno.ToString() + "," + "1" : pai.CodigoInterno.ToString().Split(',')[0] + "," + pai.CodigoInterno.ToString().Split(',')[1] + "1");
                                            else
                                            {
                                                double ultimoCodigo = pai.Filhos.Max(x => x.CodigoInterno);
                                                string codigo = (Convert.ToInt32(ultimoCodigo.ToString().Split(',')[1]) + 1).ToString();
                                                proximoCodigo = Convert.ToDouble(ultimoCodigo.ToString().Split(',')[0] + "," + codigo);
                                            }

                                            var centroCusto = new CentroCusto()
                                            {
                                                EmpresaID = upload.EmpresaID,
                                                Codigo = dr["Codigo"].ToString(),
                                                CodigoInterno = proximoCodigo,
                                                Nome = dr["Nome"].ToString(),
                                                PaiID = pai.ID
                                            };

                                            if (upload.RegraUploadID == 3)
                                                _entidadeCentroCustoRepository.Insert(centroCusto);
                                            else
                                            {
                                                var centroCustoAtualizar = centrosCusto.Where(x => x.Codigo == centroCusto.Codigo).SingleOrDefault();

                                                if (centroCustoAtualizar == null)
                                                    _entidadeCentroCustoRepository.Insert(centroCusto);
                                                else
                                                {
                                                    if (upload.RegraUploadID == 1)
                                                    {
                                                        centroCustoAtualizar.Nome = centroCusto.Nome;

                                                        _entidadeCentroCustoRepository.Update(centroCustoAtualizar);
                                                    }
                                                }
                                            }

                                            dr["ID"] = centroCusto.ID;

                                            centrosCusto = _entidadeCentroCustoRepository.GetByEmpresaID(upload.EmpresaID);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // UPLOAD DE CONTAS CONTÁBEIS
                if (upload.Entidade == Enums.Entidade.CONTA_CONTABIL)
                {
                    if (upload.RegraUploadID == 3)
                        _entidadeCentroCustoRepository.DeleteAll(upload.EmpresaID);

                    List<ContaContabil> contasContabeis = _entidadeContaContabilRepository.GetByEmpresaID(upload.EmpresaID);

                    dadosOK.Columns.Add(new DataColumn("PaiID"));
                    dadosOK.Columns.Add(new DataColumn("ID"));
                    dadosOK.Columns.Add(new DataColumn("Critica"));

                    foreach (DataRow dr in dadosOK.Rows)
                    {
                        string critica = string.Empty;

                        if (!string.IsNullOrEmpty(dr["CodigoPai"].ToString()))
                        {
                            if (dr["CodigoPai"].ToString() == dr["Codigo"].ToString())
                                critica += "CodigoPai e Codigo estão com o mesmo valor; ";
                            else
                            {
                                var contaContabil = contasContabeis.Where(x => x.Codigo == dr["CodigoPai"].ToString()).SingleOrDefault();

                                if (contaContabil == null)
                                {
                                    if (!dadosOK.Select($"[Codigo] = '{dr["CodigoPai"].ToString()}'").Any())
                                        critica += "CodigoPai sem referência na Base de Dados e nem no Arquivo de Upload; ";
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(critica))
                        {
                            dr["Critica"] = critica;

                            DataRow rowInvalido;

                            rowInvalido = dadosInvalidos.NewRow();

                            rowInvalido["Codigo"] = dr["Codigo"];
                            rowInvalido["Nome"] = dr["Nome"];
                            rowInvalido["NomeAbreviado"] = dr["NomeAbreviado"];
                            rowInvalido["CodigoPai"] = dr["CodigoPai"];
                            rowInvalido["Critica"] = critica;

                            dadosInvalidos.Rows.Add(rowInvalido);
                        }
                    }

                    DataRow[] rowsNaoCriticados = dadosOK.Select("LEN([Critica]) IS NULL");

                    if (rowsNaoCriticados.Any())
                    {
                        DataTable dtBase = rowsNaoCriticados.CopyToDataTable();

                        while (dtBase.Select("LEN([ID]) IS NULL").Any())
                        {
                            foreach (DataRow dr in dtBase.Rows)
                            {
                                if (string.IsNullOrEmpty(dr["ID"].ToString()))
                                {
                                    if (string.IsNullOrEmpty(dr["CodigoPai"].ToString()))
                                    {
                                        var contaContabil = new ContaContabil()
                                        {
                                            EmpresaID = upload.EmpresaID,
                                            Codigo = dr["Codigo"].ToString(),
                                            CodigoInterno = contasContabeis.Where(x => x.PaiID == null).Count() == 0 ? 1 : contasContabeis.Where(x => x.PaiID == null).Max(x => x.CodigoInterno) + 1,
                                            Nome = dr["Nome"].ToString(),
                                            NomeAbreviado = dr["NomeAbreviado"].ToString()
                                        };

                                        if (upload.RegraUploadID == 3)
                                            _entidadeContaContabilRepository.Insert(contaContabil);
                                        else
                                        {
                                            var contaContabilAtualizar = contasContabeis.Where(x => x.Codigo == contaContabil.Codigo).SingleOrDefault();

                                            if (contaContabilAtualizar == null)
                                                _entidadeContaContabilRepository.Insert(contaContabil);
                                            else
                                            {
                                                if (upload.RegraUploadID == 1)
                                                {
                                                    contaContabilAtualizar.Nome = contaContabil.Nome;
                                                    contaContabilAtualizar.NomeAbreviado = contaContabil.NomeAbreviado;

                                                    _entidadeContaContabilRepository.Update(contaContabilAtualizar);
                                                }
                                            }
                                        }

                                        dr["ID"] = contaContabil.ID;

                                        contasContabeis = _entidadeContaContabilRepository.GetByEmpresaID(upload.EmpresaID);
                                    }
                                    else
                                    {
                                        var pai = contasContabeis.Where(x => x.Codigo == dr["CodigoPai"].ToString()).SingleOrDefault();

                                        if (pai != null)
                                        {
                                            double proximoCodigo = 0;

                                            if (pai.Filhos.Count == 0)
                                                proximoCodigo = Convert.ToDouble(pai.CodigoInterno.ToString().Split(',').Count() == 1 ? pai.CodigoInterno.ToString() + "," + "1" : pai.CodigoInterno.ToString().Split(',')[0] + "," + pai.CodigoInterno.ToString().Split(',')[1] + "1");
                                            else
                                            {
                                                double ultimoCodigo = pai.Filhos.Max(x => x.CodigoInterno);
                                                string codigo = (Convert.ToInt32(ultimoCodigo.ToString().Split(',')[1]) + 1).ToString();
                                                proximoCodigo = Convert.ToDouble(ultimoCodigo.ToString().Split(',')[0] + "," + codigo);
                                            }

                                            var contaContabil = new ContaContabil()
                                            {
                                                EmpresaID = upload.EmpresaID,
                                                Codigo = dr["Codigo"].ToString(),
                                                CodigoInterno = proximoCodigo,
                                                Nome = dr["Nome"].ToString(),
                                                NomeAbreviado = dr["NomeAbreviado"].ToString(),
                                                PaiID = pai.ID
                                            };

                                            if (upload.RegraUploadID == 3)
                                                _entidadeContaContabilRepository.Insert(contaContabil);
                                            else
                                            {
                                                var contaContabilAtualizar = contasContabeis.Where(x => x.Codigo == contaContabil.Codigo).SingleOrDefault();

                                                if (contaContabilAtualizar == null)
                                                    _entidadeContaContabilRepository.Insert(contaContabil);
                                                else
                                                {
                                                    if (upload.RegraUploadID == 1)
                                                    {
                                                        contaContabilAtualizar.Nome = contaContabil.Nome;
                                                        contaContabilAtualizar.NomeAbreviado = contaContabil.NomeAbreviado;

                                                        _entidadeContaContabilRepository.Update(contaContabilAtualizar);
                                                    }
                                                }
                                            }

                                            dr["ID"] = contaContabil.ID;

                                            contasContabeis = _entidadeContaContabilRepository.GetByEmpresaID(upload.EmpresaID);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (dadosInvalidos.Rows.Count > 0)
                {
                    var pathCriticas = System.Configuration.ConfigurationManager.AppSettings["Uploads"] + string.Format("{0}\\Criticas\\", upload.ID);

                    if (!Directory.Exists(pathCriticas))
                        Directory.CreateDirectory(pathCriticas);

                    pathCriticas += "CRT_" + upload.NomeArquivo;

                    using (StreamWriter sw = new StreamWriter(pathCriticas, true, Encoding.UTF8))
                    {
                        string texto = string.Empty;

                        foreach (var col in dadosInvalidos.Columns)
                        {
                            texto += col.ToString() + ";";
                        }

                        texto = texto.Substring(0, texto.Length - 1);

                        sw.WriteLine(texto);

                        texto = string.Empty;

                        foreach (DataRow dr in dadosInvalidos.Rows)
                        {
                            foreach (var col in dadosInvalidos.Columns)
                            {
                                texto += dr[col.ToString()] + ";";
                            }

                            texto = texto.Substring(0, texto.Length - 1);

                            sw.WriteLine(texto);

                            texto = string.Empty;
                        }
                    }

                    upload.NomeArquivoCriticas = "CRT_" + upload.NomeArquivo;
                    upload.Status = Enums.StatusUpload.PROCESSADO_COM_CRITICAS;
                }
                else
                {
                    upload.Status = Enums.StatusUpload.PROCESSADO_OK;
                    upload.NomeArquivoCriticas = string.Empty;
                }

                upload.Observacoes = string.Empty;

                _repository.Update(upload);

                return Request.CreateResponse(HttpStatusCode.OK, "Upload Processado com Sucesso");
            }
            catch (Exception ex)
            {
                upload.Status = Enums.StatusUpload.ERRO;
                upload.Observacoes = ex.Message;

                _repository.Update(upload);

                return Request.CreateResponse(HttpStatusCode.OK, "Erro ao Processar Arquivo de Upload");
            }
        }
    }
}
