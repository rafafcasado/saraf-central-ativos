using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Interfaces;
using System.Linq;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class MenuController : ApiController
    {
        private readonly IMenu _repository;
        private readonly IPerfilMenu _perfilMenuRepository;

        public MenuController(IMenu repository, IPerfilMenu perfilMenuRepository)
        {
            _repository = repository;
            _perfilMenuRepository = perfilMenuRepository;
        }

        /// <summary>
        /// Retorna Lista de Menus Cadastrados
        /// </summary>
        [HttpGet]
        [Route("api/menu/getMenus")]
        public IHttpActionResult GetMenus()
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
        /// Retorna Lista de Menus de acordo com o ID do Perfil informado
        /// </summary>
        /// <param name="perfilID">ID do Perfil</param>
        [HttpGet]
        [Route("api/menu/getByPerfilID/{perfilID}")]
        public IHttpActionResult GetByPerfilID(int perfilID)
        {
            try
            {
                return Ok(_perfilMenuRepository.GetByPerfilID(perfilID).OrderBy(x => x.Menu.TipoMenu).ThenBy(x => x.Menu.TituloMenu));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}