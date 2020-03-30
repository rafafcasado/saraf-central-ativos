using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class OrdemServicoController : ApiController
    {
        private readonly IItem _itRepository;
        private readonly IResponsavel _responsavelRepository;
        private readonly IOrdemServico _repository;
        private readonly IOrdemServicoAnexo _anexoRepository;
        private readonly IOrdemServicoCampo _campoRepository;
        private readonly IOrdemServicoItem _itemRepository;
        private readonly IOrdemServicoMotivoCampo _motivoCampoRepository;
        private readonly IOrdemServicoMotivoCampoValor _valorRepository;

        public OrdemServicoController(IOrdemServico repository, IOrdemServicoAnexo anexoRepository, IOrdemServicoCampo campoRepository, IOrdemServicoMotivoCampo motivoCampoRepository, IOrdemServicoMotivoCampoValor valorRepository, IOrdemServicoItem itemRepository, IResponsavel responsavelRepository, IItem itRepository)
        {
            _repository = repository;
            _anexoRepository = anexoRepository;
            _campoRepository = campoRepository;
            _motivoCampoRepository = motivoCampoRepository;
            _valorRepository = valorRepository;
            _itemRepository = itemRepository;
            _responsavelRepository = responsavelRepository;
            _itRepository = itRepository;
        }

        /// <summary>
        /// Retorna Lista de Ordens de Serviço Cadastradas
        /// </summary>
        /// /// <param name="empresaID">ID da Empresa</param>
        [HttpGet]
        [Route("api/ordemservico/getordensservico/{empresaID}")]
        public IHttpActionResult GetOrdensServico(int empresaID)
        {
            try
            {
                List<OrdemServico> ordens = _repository.GetByEmpresaID(empresaID);

                List<object> ordensRetono = new List<object>();

                foreach (var ordem in ordens)
                {
                    var ordemServicoMotivoCampos = _motivoCampoRepository.GetByMotivoID(ordem.OrdemServicoMotivoID);

                    IDictionary camposOS = new Dictionary<string, string>();

                    foreach (var ordemServicoMotivoCampo in ordemServicoMotivoCampos)
                    {
                        var ordermServicoMotivoCampoValor = _valorRepository.Get(ordem.ID, ordemServicoMotivoCampo.ID);

                        camposOS.Add(ordemServicoMotivoCampo.OrdemServicoCampo.NomeCampo, ordermServicoMotivoCampoValor.Valor);
                    }

                    var os = new
                    {
                        ordem.ID,
                        OrdemServicoMotivoNome = ordem.OrdemServicoMotivo.Nome,
                        StatusID = ordem.Status,
                        Status = ordem.Status.ToString(),
                        ordem.DataCadastroFormatada,
                        Campos = camposOS,
                    };

                    ordensRetono.Add(os);
                }

                return Ok(ordensRetono);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Ordem de Serviço relacionado ao ID informado
        /// </summary>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var ordem = _repository.GetByID(id);

                if (ordem == null)
                    return NotFound();

                var itens = _itemRepository.GetByOrdemServicoID(id);

                foreach (var orderServicoItem in itens)
                {
                    ordem.Itens.Add(orderServicoItem.Item);
                }

                var ordemServicoMotivoCampos = _motivoCampoRepository.GetByMotivoID(ordem.OrdemServicoMotivoID);

                IDictionary camposOS = new Dictionary<string, string>();

                foreach (var ordemServicoMotivoCampo in ordemServicoMotivoCampos)
                {
                    var ordermServicoMotivoCampoValor = _valorRepository.Get(id, ordemServicoMotivoCampo.ID);

                    camposOS.Add(ordemServicoMotivoCampo.OrdemServicoCampo.NomeCampo, ordermServicoMotivoCampoValor.Valor);
                }

                var os = new
                {
                    ordem.OrdemServicoMotivoID,
                    OrdemServicoMotivoNome = ordem.OrdemServicoMotivo.Nome,
                    Campos = camposOS,
                    Itens = ordem.Itens.Select(x => new { x.Codigo, x.Nome }),
                    Anexos = ordem.Anexos.Select(x => new { x.Link, x.NomeArquivo, x.Imagem })
                };

                return Ok(os);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui uma Ordem de Serviço
        /// </summary>
        /// <param name="ordemServico">Objeto do tipo OrdemServico</param>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] OrdemServico ordemServico)
        {
            try
            {
                ordemServico.Status = Enums.StatusOrdemServico.NOVO;

                _repository.Insert(ordemServico);

                InserirResponsavelOrigem(ordemServico);

                InserirCampos(ordemServico);

                InserirItens(ordemServico);

                return Request.CreateResponse(HttpStatusCode.OK, ordemServico);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Inclui Anexos à Ordem de Servico
        /// </summary>
        /// <param name="ordemServicoID">ID da Ordem de Servico</param>
        [HttpPut, Route("api/ordemservico/enviaranexos/{ordemServicoID}")]
        public HttpResponseMessage EnviarAnexos(int ordemServicoID)
        {
            try
            {
                var ordemServico = _repository.GetByID(ordemServicoID);

                if (ordemServico == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Ordem de Serviço não Localizada");

                InserirAnexosImagens(ordemServico);

                return Request.CreateResponse(HttpStatusCode.OK, "Anexos vinculados com Sucesso");
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Exclui uma Ordem de Serviço
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var ordem = _repository.GetByID(id);

                if (ordem == null)
                    return NotFound();

                _repository.Delete(id);

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza o Status de uma Ordem de Serviço
        /// </summary>
        /// <param name="id">ID da Ordem de Serviço</param>
        /// <param name="aprovado">Booleano informando se está aprovado</param>
        [HttpPut, Route("api/ordemservico/alterastatus/{id}/{aprovado}")]
        public IHttpActionResult AlteraStatus(int id, bool aprovado)
        {
            try
            {
                var os = _repository.GetByID(id);

                if (os == null)
                    return NotFound();

                if (aprovado)
                {
                    os.Status = Enums.StatusOrdemServico.APROVADO;

                    if (os.OrdemServicoMotivo.AcaoFinal == Enums.AcaoFinalOrdemServico.EXCLUIR_ITEM)
                    {
                        var osItens = _itemRepository.GetByOrdemServicoID(os.ID);

                        foreach (var item in osItens)
                        {
                            _itRepository.Delete(item.ItemID);
                        }
                    }

                    if (os.OrdemServicoMotivo.AcaoFinal == Enums.AcaoFinalOrdemServico.ATUALIZAR_LOCAL_DESTINO)
                    {
                        var osMotivoID = os.OrdemServicoMotivoID;

                        var osCampoLocalDestinoID = _campoRepository.GetByName("LocalDestinoID").ID;

                        var osMotivoCampoLocalDestinoID = _motivoCampoRepository.GetByMotivoCampo(osMotivoID, osCampoLocalDestinoID).ID;

                        var osMotivoCampoValorLocalDestino = Convert.ToInt32(_valorRepository.Get(os.ID, osMotivoCampoLocalDestinoID).Valor);

                        var osItens = _itemRepository.GetByOrdemServicoID(os.ID);

                        foreach (var osItem in osItens)
                        {
                            var item = osItem.Item;

                            item.LocalID = osMotivoCampoValorLocalDestino;

                            _itRepository.Update(item, true);
                        }

                    }
                }
                else
                    os.Status = Enums.StatusOrdemServico.REPROVADO;

                _repository.Update(os);

                return Ok(os);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        protected void InserirResponsavelOrigem(OrdemServico ordemServico)
        {
            var httpRequest = HttpContext.Current.Request;

            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

            var campoResponsavelOrigem = _campoRepository.GetByName("ResponsavelOrigemID");

            var motivoCampoResponsavelOrigem = _motivoCampoRepository.GetByMotivoCampo(ordemServico.OrdemServicoMotivoID, campoResponsavelOrigem.ID);

            var usuarioToken = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == "UsuarioID").Value);

            var responsavel = _responsavelRepository.GetByUsuarioID(usuarioToken);

            _valorRepository.Insert(new OrdemServicoMotivoCampoValor() { OrdemServicoID = ordemServico.ID, OrdemServicoMotivoCampoID = motivoCampoResponsavelOrigem.ID, Valor = responsavel.ID.ToString() });
        }

        protected void InserirCampos(OrdemServico ordemServico)
        {
            var motivoCampos = _motivoCampoRepository.GetByMotivoID(ordemServico.OrdemServicoMotivoID);

            foreach (var motivoCampo in motivoCampos)
            {
                var valor = ordemServico.Campos[motivoCampo.OrdemServicoCampo.NomeCampo];

                if (!string.IsNullOrEmpty(valor))
                    _valorRepository.Insert(new OrdemServicoMotivoCampoValor() { OrdemServicoID = ordemServico.ID, OrdemServicoMotivoCampoID = motivoCampo.ID, Valor = valor });
            }
        }

        protected void InserirItens(OrdemServico ordemServico)
        {
            foreach (var item in ordemServico.Itens)
            {
                _itemRepository.Insert(new OrdemServicoItem
                {
                    ItemID = item.ID,
                    OrdemServicoID = ordemServico.ID
                });
            }
        }

        protected void InserirAnexosImagens(OrdemServico ordemServico)
        {
            string[] imageExtensions = { ".jpg", ".png", ".jpeg", ".gif" };
            var isImage = false;

            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count > 0)
            {
                for (int i = 0; i < httpRequest.Files.Count; i++)
                {
                    var postedFile = httpRequest.Files[i];

                    FileInfo fi = new FileInfo(postedFile.FileName);

                    if (imageExtensions.Contains(fi.Extension.ToLower()))
                        isImage = true;

                    _anexoRepository.Insert(new OrdemServicoAnexo() { Imagem = isImage, NomeArquivo = postedFile.FileName, OrdemServicoID = ordemServico.ID });

                    var filePath = System.Configuration.ConfigurationManager.AppSettings["OrdensServico"] + ordemServico.ID + "\\";

                    if (isImage)
                        filePath += "Imagem\\";
                    else
                        filePath += "Anexo\\";

                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    filePath += postedFile.FileName;

                    postedFile.SaveAs(filePath);
                }
            }
        }
    }
}
