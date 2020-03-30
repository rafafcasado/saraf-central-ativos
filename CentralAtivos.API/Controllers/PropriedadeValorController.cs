using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class PropriedadeValorController : ApiController
    {
        private readonly IPropriedadeValor _repository;

        public PropriedadeValorController(IPropriedadeValor repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Exclui um Valor vinculado à Propriedade
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var valor = _repository.GetByID(id);

                if (valor == null)
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
