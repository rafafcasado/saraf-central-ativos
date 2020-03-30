using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class InventarioLocalController : ApiController
    {
        private readonly IInventarioLocal _repository;

        public InventarioLocalController(IInventarioLocal repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retorna Lista de Locais atribuidos ao Inventário de acordo com o ID informado
        /// </summary>
        /// <param name="inventarioID">ID do Inventário</param>
        [HttpGet]
        [Route("api/inventarioLocal/getByInventarioID/{inventarioID}")]
        public IHttpActionResult GetByInventarioID(int inventarioID)
        {
            try
            {
                return Ok(_repository.GetByInventarioID(inventarioID));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui um Local ao Inventário
        /// </summary>
        /// <param name="inventarioLocal">Objeto do tipo InventarioLocal</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] InventarioLocal inventarioLocal)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _repository.Insert(inventarioLocal);

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um Local do Inventário
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var inventarioLocal = _repository.GetByID(id);

                if (inventarioLocal == null)
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
