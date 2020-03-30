using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class SincronizacaoController : ApiController
    {
        private readonly IEmpresa _empresaRepository;
        private readonly IEspecie _especieRepository;
        private readonly IInventario _inventarioRepository;
        private readonly IItem _itemRepository;
        private readonly IItemEstado _itemEstadoRepository;
        private readonly ILocal _localRepository;
        private readonly IResponsavel _responsavelRepository;
        private readonly ISincronizacao _repository;

        public SincronizacaoController(ISincronizacao repository, IItem itemRepository, ILocal localRepository, IInventario inventarioRepository, IEmpresa empresaRepository, IItemEstado itemEstadoRepository, IEspecie especieRepository)
        {
            _repository = repository;
            _itemRepository = itemRepository;
            _localRepository = localRepository;
            _inventarioRepository = inventarioRepository;
            _empresaRepository = empresaRepository;
            _itemEstadoRepository = itemEstadoRepository;
            _especieRepository = especieRepository;
        }

        /// <summary>
        /// Retorna Lista de Sincronizações
        /// </summary>
        [HttpGet]
        [Route("api/sincronizacao/getSincronizacoes")]
        public IHttpActionResult GetSincronizacoes()
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
        /// Envia Arquivo de Sincronização
        /// </summary>
        /// <param name="inventarioID">ID do Inventário</param>
        [HttpPost, Route("api/sincronizacao/enviar/{inventarioID}")]
        public HttpResponseMessage Post(int inventarioID)
        {
            try
            {
                HttpResponseMessage result = null;

                var httpRequest = HttpContext.Current.Request;

                if (httpRequest.Files.Count > 0)
                {
                    var postedFile = httpRequest.Files[0];
                    var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

                    var sincronizacao = new Sincronizacao()
                    {
                        DataEnvioArquivo = DateTime.Now,
                        InventarioID = inventarioID,
                        Status = Enums.StatusSincronizacao.NOVA,
                        UsuarioEnvioID = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == "UsuarioID").Value)
                    };

                    _repository.Insert(sincronizacao);

                    var filePath = System.Configuration.ConfigurationManager.AppSettings["Sincronizacoes"] + sincronizacao.ID + "\\";

                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    filePath += postedFile.FileName;

                    postedFile.SaveAs(filePath);

                    result = Request.CreateResponse(HttpStatusCode.OK, sincronizacao);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                return result;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Inicia Processamento de Arquivo de Sincronização
        /// </summary>
        [HttpPost, Route("api/sincronizacao/processar/{id}")]
        public HttpResponseMessage Processar(int id)
        {
            var sinc = _repository.GetByID(id);

            if (sinc == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Sincronização não Localizada");

            sinc.Status = Enums.StatusSincronizacao.PROCESSANDO;
            _repository.Update(sinc);

            try
            {
                var filePath = System.Configuration.ConfigurationManager.AppSettings["Sincronizacoes"] + sinc.ID + "\\sinc.json";

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string jsonString = sr.ReadToEnd();

                    JavaScriptSerializer jss = new JavaScriptSerializer();

                    List<Item> itens = jss.Deserialize<List<Item>>(jsonString);

                    var pathCriticas = System.Configuration.ConfigurationManager.AppSettings["Sincronizacoes"] + sinc.ID + "\\criticas.txt";

                    if (File.Exists(pathCriticas))
                        File.Delete(pathCriticas);

                    if (itens.Count > 0)
                    {
                        var contadorCriticas = 0;
                        var inventario = _inventarioRepository.GetByID(sinc.InventarioID);
                        var locais = _localRepository.GetByEmpresaID(inventario.EmpresaID);
                        var empresas = _empresaRepository.GetAll();
                        var itemEstados = _itemEstadoRepository.GetAll();
                        var responsaveis = _responsavelRepository.GetByEmpresaID(inventario.EmpresaID);
                        var especies = _especieRepository.GetByEmpresaID(inventario.EmpresaID);

                        foreach (var item in itens)
                        {
                            bool critica = false;

                            var itemEstado = itemEstados.Where(x => x.ID == item.ItemEstadoID).SingleOrDefault();

                            if (itemEstado == null)
                            {
                                using (var sw = new StreamWriter(pathCriticas))
                                {
                                    sw.WriteLine($"ItemEstado informado para o Item {item.Nome} não existe na Base de Dados;");
                                }

                                critica = true;
                            }

                            if (item.ResponsavelID != null)
                            {
                                var responsavel = responsaveis.Where(x => x.ID == item.ResponsavelID).SingleOrDefault();

                                if (responsavel == null)
                                {
                                    using (var sw = new StreamWriter(pathCriticas))
                                    {
                                        sw.WriteLine($"Responsável informado para o Item {item.Nome} não existe na Base de Dados;");
                                    }

                                    critica = true;
                                }
                            }                            

                            var local = locais.Where(x => x.ID == item.LocalID).SingleOrDefault();

                            if (local == null)
                            {
                                using (var sw = new StreamWriter(pathCriticas))
                                {
                                    sw.WriteLine($"Local informado para o Item {item.Nome} não existe na Base de Dados;");
                                }

                                critica = true;
                            }

                            var especie = especies.Where(x => x.ID == item.EspecieID).SingleOrDefault();

                            if (especie == null)
                            {
                                using (var sw = new StreamWriter(pathCriticas))
                                {
                                    sw.WriteLine($"Espécie informada para o Item {item.Nome} não existe na Base de Dados;");
                                }

                                critica = true;
                            }

                            var empresa = empresas.Where(x => x.ID == item.EmpresaID).SingleOrDefault();

                            if (empresa == null)
                                item.EmpresaID = inventario.EmpresaID;

                            if (item.Incorporacao != null)
                            {
                                if (!int.TryParse(item.Incorporacao.ToString(), out int incorporacao))
                                {
                                    using (var sw = new StreamWriter(pathCriticas))
                                    {
                                        sw.WriteLine($"Incorporação informada para o Item {item.Nome} precisa ser um INT;");
                                    }

                                    critica = true;
                                }
                            }

                            if (item.IncorporacaoAnterior != null)
                            {
                                if (!int.TryParse(item.IncorporacaoAnterior.ToString(), out int incorporacaoAnterior))
                                {
                                    using (var sw = new StreamWriter(pathCriticas))
                                    {
                                        sw.WriteLine($"Incorporação Anterior informada para o Item {item.Nome} precisa ser um INT;");
                                    }

                                    critica = true;
                                }
                            }

                            if (item.Latitude != null)
                            {
                                if (!decimal.TryParse(item.Latitude.ToString(), out decimal latitude))
                                {
                                    using (var sw = new StreamWriter(pathCriticas))
                                    {
                                        sw.WriteLine($"Latitude informada para o Item {item.Nome} precisa ser um DECIMAL;");
                                    }

                                    critica = true;
                                }
                            }

                            if (item.Longitude != null)
                            {
                                if (!decimal.TryParse(item.Longitude.ToString(), out decimal longitude))
                                {
                                    using (var sw = new StreamWriter(pathCriticas))
                                    {
                                        sw.WriteLine($"Longitude informada para o Item {item.Nome} precisa ser um DECIMAL;");
                                    }

                                    critica = true;
                                }
                            }

                            if (critica)
                                contadorCriticas += 1;

                        }

                        if (contadorCriticas > 0)
                        {
                            sinc.Status = Enums.StatusSincronizacao.CRITICAS;

                            _repository.Update(sinc);

                            return Request.CreateResponse(HttpStatusCode.OK, "Foram encontradas incosistências nos dados enviados. Trate os dados e reprocesse a Sincronização.");
                        }
                        else
                        {
                            foreach (var item in itens)
                            {
                                _itemRepository.Insert(item);
                            }

                            return Request.CreateResponse(HttpStatusCode.OK, "Sincronização Processada com Sucesso.");
                        }
                    }
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, "Não foram encontrados Itens para Sincronizar!");
                }
            }
            catch (Exception ex)
            {
                sinc.Status = Enums.StatusSincronizacao.ERRO;

                _repository.Update(sinc);

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
