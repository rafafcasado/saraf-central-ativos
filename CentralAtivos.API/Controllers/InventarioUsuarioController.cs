using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class InventarioUsuarioController : ApiController
    {
        private readonly IInventarioUsuario _repository;
        private readonly IInventario _inventarioRepository;

        public InventarioUsuarioController(IInventarioUsuario repository, IInventario inventarioRepository)
        {
            _repository = repository;
            _inventarioRepository = inventarioRepository;
        }

        /// <summary>
        /// Retorna Lista de Usuários atribuidos ao Inventário de acordo com o ID informado
        /// </summary>
        /// <param name="inventarioID">ID do Inventário</param>
        [HttpGet]
        [Route("api/inventarioUsuario/getByInventarioID/{inventarioID}")]
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
        /// Retorna Lista dos Inventarios de acordo com o ID do Usuário vinculado
        /// </summary>
        /// <param name="usuarioID">ID do Usuário</param>
        [HttpGet]
        [Route("api/inventarioUsuario/getByUsuarioID/{usuarioID}")]
        public IHttpActionResult GetByUsuarioID(int usuarioID)
        {
            try
            {
                List<object> inventarios = new List<object>();

                foreach (var item in _inventarioRepository.GetByInventarioUsuarioID(usuarioID))
                {
                    inventarios.Add(new
                    {
                        item.Codigo,
                        item.DataCadastroFormatada,
                        item.EmpresaID,
                        item.Geral,
                        item.ID,
                        item.Nome,
                        item.StatusID,
                        StatusNome = item.Status.Nome,
                    });
                }

                return Ok(inventarios);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui um Usuário à Equipe do Inventário
        /// </summary>
        /// <param name="inventarioUsuario">Objeto do tipo InventarioUsuario</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] InventarioUsuario inventarioUsuario)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _repository.Insert(inventarioUsuario);

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um Usuário do Inventário
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var inventarioUsuario = _repository.GetByID(id);

                if (inventarioUsuario == null)
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
