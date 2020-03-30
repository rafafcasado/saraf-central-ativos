using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class EspecieController : ApiController
    {
        private readonly IEspecie _repository;

        public EspecieController(IEspecie repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retorna Lista dos Espécies Ativas Cadastrados para um Grupo de acordo com o ID informado
        /// </summary>
        /// <param name="grupoID">ID do Grupo</param>
        [HttpGet]
        [Route("api/especie/getByGrupoID/{grupoID}")]
        public IHttpActionResult GetByGrupoID(int grupoID)
        {
            try
            {
                var especies = _repository.GetByGrupoID(grupoID);

                return Ok(especies);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Lista dos Espécies Ativas Cadastradas de acordo com a empresa informada
        /// </summary>
        /// <param name="empresaID">ID da Empresa</param>
        [HttpGet]
        [Route("api/especie/getByEmpresaID/{empresaID}")]
        public IHttpActionResult GetByEmpresaID(int empresaID)
        {
            try
            {
                var especies = _repository.GetByEmpresaID(empresaID);

                return Ok(especies);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Espécie relacionada ao ID informado
        /// </summary>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var especie = _repository.GetByID(id);

                if (especie == null)
                    return NotFound();

                return Ok(especie);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui uma Espécie
        /// </summary>
        /// <param name="especie">Objeto do tipo Espécie</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] Especie especie)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _repository.Insert(especie);

                return Ok(especie);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de uma Espécie
        /// </summary>
        /// <param name="especie">Objeto do tipo Espécie</param>
        /// <param name="id">ID da Espécie que será atualizada</param>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Especie especie)
        {
            try
            {
                var especieDB = _repository.GetByID(id);

                if (especieDB == null)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                especieDB.GrupoID = especie.GrupoID;
                especieDB.Codigo = especie.Codigo;
                especieDB.Nome = especie.Nome;

                _repository.Update(especieDB);

                return Ok(especieDB);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui uma Espécie
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var especie = _repository.GetByID(id);

                if (especie == null)
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
