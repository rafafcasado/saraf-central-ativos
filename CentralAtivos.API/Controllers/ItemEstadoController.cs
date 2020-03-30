using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Interfaces;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class ItemEstadoController : ApiController
    {
        private readonly IItemEstado _repository;

        public ItemEstadoController(IItemEstado repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retorna Lista de Todos os Estados dos Itens
        /// </summary>
        [HttpGet]
        [Route("api/itemEstado/getAll")]
        public IHttpActionResult GetAll()
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
