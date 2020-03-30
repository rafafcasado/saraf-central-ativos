using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class EspeciePropriedadeController : ApiController
    {
        private readonly IEspeciePropriedade _repository;

        public EspeciePropriedadeController(IEspeciePropriedade repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retorna Lista dos Propriedades vinculadas à Espécie de acordo com ID
        /// </summary>
        /// <param name="especieID">ID da Espécie</param>
        [HttpGet]
        [Route("api/especiePropriedade/getByEspecieID/{especieID}")]
        public IHttpActionResult GetByEspecieID(int especieID)
        {
            try
            {
                var especiePropriedades = _repository.GetByEspecieID(especieID);

                return Ok(especiePropriedades);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui vínculo Espécie Propriedades
        /// </summary>
        /// <param name="especiePropriedades">Array de Objetos do tipo EspeciePropriedade</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] List<EspeciePropriedade> especiePropriedades)
        {
            try
            {
                foreach (var item in especiePropriedades)
                {
                    if (ModelState.IsValid)
                        _repository.Insert(item);
                }

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um vínculo Espécie Propriedade
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var especiePropriedade = _repository.GetByID(id);

                if (especiePropriedade == null)
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
