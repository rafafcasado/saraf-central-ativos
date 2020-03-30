using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class InventarioFilialController : ApiController
    {
        private readonly IInventarioFilial _repository;

        public InventarioFilialController(IInventarioFilial repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retorna Lista de Filiais atribuidas ao Inventário de acordo com o ID informado
        /// </summary>
        /// <param name="inventarioID">ID do Inventário</param>
        [HttpGet]
        [Route("api/inventarioFilial/getByInventarioID/{inventarioID}")]
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
        /// Inclui uma Filial ao Inventário
        /// </summary>
        /// <param name="inventarioFilial">Objeto do tipo InventarioFilial</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] InventarioFilial inventarioFilial)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _repository.Insert(inventarioFilial);

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui uma Filial do Inventário
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var inventarioFilial = _repository.GetByID(id);

                if (inventarioFilial == null)
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
