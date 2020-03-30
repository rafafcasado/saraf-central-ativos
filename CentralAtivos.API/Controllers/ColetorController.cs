using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class ColetorController : ApiController
    {
        private readonly IColetor _repository;

        public ColetorController(IColetor repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retorna Lista de Coletores Cadastrados
        /// </summary>
        [HttpGet]
        [Route("api/coletor/getColetores")]
        public IHttpActionResult GetColetores()
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
        /// Retorna Coletor relacionado ao ID informado
        /// </summary>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var coletor = _repository.GetByID(id);

                if (coletor == null)
                    return NotFound();

                return Ok(coletor);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui um Coletor
        /// </summary>
        /// <param name="coletor">Objeto do tipo Coletor</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] Coletor coletor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Verificar campos obrigatórios");

                _repository.Insert(coletor);

                return Ok(coletor);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de um Coletor
        /// </summary>
        /// <param name="id">ID do Coletor que será atualizado</param>
        /// <param name="coletor">Objeto do tipo Coletor</param>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Coletor coletor)
        {
            try
            {
                var coletorDB = _repository.GetByID(id);

                if (coletorDB == null)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest("Verificar campos obrigatórios");

                coletorDB.Codigo = coletor.Codigo;
                coletorDB.NumeroSerie = coletor.NumeroSerie;
                coletorDB.Marca = coletor.Marca;
                coletorDB.Modelo = coletor.Modelo;
                coletorDB.Nome = coletor.Nome;

                _repository.Update(coletorDB);

                return Ok(coletorDB);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um Coletor
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var coletor = _repository.GetByID(id);

                if (coletor == null)
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
