using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Linq;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class GrupoController : ApiController
    {
        private readonly IGrupo _repository;

        public GrupoController(IGrupo repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retorna Lista dos Grupos Ativos Cadastrados para uma Empresa de acordo com o ID informado
        /// </summary>
        /// <param name="empresaID">ID da Empresa</param>
        [HttpGet]
        [Route("api/grupo/getByEmpresaID/{empresaID}")]
        public IHttpActionResult GetByEmpresaID(int empresaID)
        {
            try
            {
                var grupos = _repository.GetByEmpresaID(empresaID);

                grupos = grupos.ToList();

                return Ok(grupos.OrderBy(x => x.Codigo).ThenBy(x => x.Nome));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Grupo relacionado ao ID informado
        /// </summary>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var grupo = _repository.GetByID(id);

                if (grupo == null)
                    return NotFound();

                return Ok(grupo);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui um Grupo
        /// </summary>
        /// <param name="grupo">Objeto do tipo Grupo</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] Grupo grupo)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _repository.Insert(grupo);

                return Ok(grupo);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de um Grupo
        /// </summary>
        /// <param name="grupo">Objeto do tipo Grupo</param>
        /// <param name="id">ID do Grupo que será atualizado</param>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Grupo grupo)
        {
            try
            {
                var grupoDB = _repository.GetByID(id);

                if (grupoDB == null)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                grupoDB.ContaContabilID = grupo.ContaContabilID;
                grupoDB.EmpresaID = grupo.EmpresaID;
                grupoDB.Codigo = grupo.Codigo;
                grupoDB.Nome = grupo.Nome;

                _repository.Update(grupoDB);

                return Ok(grupoDB);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um Grupo
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var grupo = _repository.GetByID(id);

                if (grupo == null)
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
