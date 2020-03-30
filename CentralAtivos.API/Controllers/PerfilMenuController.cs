using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Linq;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class PerfilMenuController : ApiController
    {
        private readonly IPerfilMenu _repository;

        public PerfilMenuController(IPerfilMenu repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Vincula um Menu ao Perfil
        /// </summary>
        /// <param name="perfilMenu">Objeto do tipo PerfilMenu</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] PerfilMenu perfilMenu)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Verificar campos obrigatórios");

                _repository.Insert(perfilMenu);

                return Ok(perfilMenu);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Desvincula um Menu do Perfil
        /// </summary>
        /// <param name="menuID">ID do Menu</param>
        /// <param name="perfilID">ID do Perfil</param>
        [HttpDelete, Route("api/perfilmenu/delete/{menuID}/{perfilID}")]
        public IHttpActionResult Delete(int menuID, int perfilID)
        {
            try
            {
                var lista = _repository.GetByPerfilID(perfilID);

                var perfilMenu = lista.Where(x => x.MenuID == menuID).SingleOrDefault();

                if (perfilMenu == null)
                    return NotFound();

                _repository.Delete(perfilMenu.ID);

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
