using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Interfaces;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class OrdemServicoMotivoController : ApiController
    {
        private readonly IOrdemServicoMotivo _repository;

        public OrdemServicoMotivoController(IOrdemServicoMotivo repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retorna Lista de Motivos de Ordem de Serviço Cadastrados
        /// </summary>
        [HttpGet]
        [Route("api/ordemservicomotivo/getmotivos")]
        public IHttpActionResult GetMotivos()
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
    }
}
