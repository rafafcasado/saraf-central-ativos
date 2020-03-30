using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class ItemController : ApiController
    {
        private readonly IItem _repository;
        private readonly ILocal _localRepository;
        private readonly IEspecie _especieRepository;

        public ItemController(IItem repository, ILocal localRepository, IEspecie especieRepository)
        {
            _repository = repository;
            _localRepository = localRepository;
            _especieRepository = especieRepository;
        }

        /// <summary>
        /// Retorna Lista de Itens Cadastrados para a Empresa de acordo com o ID informado
        /// </summary>
        /// <param name="empresaID">ID da Empresa</param>
        [HttpGet]
        [Route("api/item/getByEmpresaID/{empresaID}")]
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
        /// Retorna Lista de Itens Cadastrados para a Empresa de acordo com o LocalID informado
        /// </summary>
        /// <param name="localID">ID do Local</param>
        [HttpGet]
        [Route("api/item/getByLocalID/{localID}")]
        public IHttpActionResult GetByLocalID(int localID)
        {
            try
            {
                return Ok(_repository.GetByLocalID(localID));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Item relacionado ao ID informado
        /// </summary>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {                
                var item = _repository.GetByID(id);

                if (item == null)
                    return NotFound();

                var locais = _localRepository.GetByEmpresaID(item.EmpresaID);

                bool fim = false;
                var pai = item.Local.PaiID == null ? null : locais.Where(x => x.ID == item.Local.PaiID).SingleOrDefault();

                while (!fim)
                {
                    if (pai == null)
                    {
                        fim = true;
                        item.Local.CaminhoPao = item.Local.CaminhoPao == null || item.Local.CaminhoPao.Length <= 0 ? item.Local.Nome : (item.Local.CaminhoPao + item.Local.Nome);
                    }
                    else
                    {

                        fim = false;
                        item.Local.CaminhoPao = pai.Nome + " > " + item.Local.CaminhoPao;
                        pai = locais.Where(x => x.ID == pai.PaiID).SingleOrDefault();
                    }
                }


                return Ok(item);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui um Item
        /// </summary>
        /// <param name="item">Objeto do tipo Item</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] Item item)
        {
            try
            {
                item.StatusID = Enums.StatusItem.INSERIDO;
                
                _repository.Insert(item);

                var path = System.Configuration.ConfigurationManager.AppSettings["GaleriaImagens"] + item.ID;

                var files = Directory.GetFiles(path);

                foreach (var file in files)
                {
                    FileInfo info = new FileInfo(file);

                    item.Imagens.Add(System.Configuration.ConfigurationManager.AppSettings["GaleriaImagensLogico"] + item.ID + "/" + info.Name);
                }

                return Ok(item);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.Exception)
            {
                return BadRequest("Erro ao Gravar Item. Favor verificar os Campos Obrigatórios e tentar novamente.");
            }
        }

        /// <summary>
        /// Atualiza os dados de um Item
        /// </summary>
        /// <param name="item">Objeto do tipo Item</param>
        /// <param name="id">ID do Item que será atualizado</param>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Item item)
        {
            try
            {
                var itemDB = _repository.GetByID(id);

                if (itemDB == null)
                    return NotFound();

                itemDB.StatusID = Enums.StatusItem.ALTERADO;
                itemDB.Codigo = item.Codigo;
                itemDB.CodigoAnterior = item.CodigoAnterior;
                itemDB.CodigoPM = item.CodigoPM;
                itemDB.DadosPM = item.DadosPM;
                itemDB.EspecieID = item.EspecieID;
                itemDB.Incorporacao = item.Incorporacao;
                itemDB.IncorporacaoAnterior = item.IncorporacaoAnterior;
                itemDB.ItemEstadoID = item.ItemEstadoID;
                itemDB.Latitude = item.Latitude;
                itemDB.LocalID = item.LocalID;
                itemDB.LocalPM = item.LocalPM;
                itemDB.Longitude = item.Longitude;
                itemDB.Marca = item.Marca;
                itemDB.Modelo = item.Modelo;
                itemDB.Nome = item.Nome;
                itemDB.NumeroSerie = item.NumeroSerie;
                itemDB.Observacao = item.Observacao;
                itemDB.ResponsavelID = item.ResponsavelID;
                itemDB.Tag = item.Tag;
                itemDB.Imagens = item.Imagens;
                itemDB.CamposExtras = item.CamposExtras;
                itemDB.PropriedadesValores = item.PropriedadesValores;

                _repository.Update(itemDB);

                var path = System.Configuration.ConfigurationManager.AppSettings["GaleriaImagens"] + itemDB.ID;

                var files = Directory.GetFiles(path);

                itemDB.Imagens = new System.Collections.Generic.List<string>();

                foreach (var file in files)
                {
                    FileInfo info = new FileInfo(file);

                    itemDB.Imagens.Add(System.Configuration.ConfigurationManager.AppSettings["GaleriaImagensLogico"] + itemDB.ID + "/" + info.Name);
                }

                return Ok(itemDB);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.Exception)
            {
                return BadRequest("Erro ao Gravar Item. Favor verificar os Campos Obrigatórios e tentar novamente.");
            }
        }

        /// <summary>
        /// Exclui um Item
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var item = _repository.GetByID(id);

                if (item == null)
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
        /// Verificar se Código + Incorporação enviada está cadastrada
        /// </summary>
        /// <param name="empresaID">ID da Empresa</param>
        /// <param name="filialID">ID da Filial</param>
        /// <param name="codigo">Código do Item</param>
        /// <param name="incorporacao">Incorporação do Item</param>
        [HttpGet]
        [Route("api/item/verifyIfExists/{empresaID}/{filialID}/{codigo}/{incorporacao}")]
        public IHttpActionResult VerifyIfExists(int empresaID, int filialID, string codigo, int incorporacao)
        {
            try
            {
                return Ok(_repository.VerifyIfExists(empresaID, filialID, codigo, incorporacao));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna o Item de acordo com o Código e Incorporação informados
        /// </summary>
        /// <param name="empresaID">ID da Empresa</param>
        /// <param name="codigo">Código do Item</param>
        /// <param name="incorporacao">Incorporação do Item</param>
        [HttpGet]
        [Route("api/item/getByCodigo/{empresaID}/{codigo}/{incorporacao}")]
        public IHttpActionResult GetByCodigo(int empresaID, string codigo, int incorporacao)
        {
            try
            {
                Item item = _repository.GetByCodigo(empresaID, codigo, incorporacao);

                if (item == null)
                    return NotFound();
                else
                    return Ok(item);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
