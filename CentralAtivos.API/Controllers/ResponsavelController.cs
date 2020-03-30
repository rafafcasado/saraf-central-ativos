using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class ResponsavelController : ApiController
    {
        private readonly IResponsavel _repository;

        public ResponsavelController(IResponsavel repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retorna Lista de Responsáveis Ativos Cadastrados para a Empresa de acordo com o ID informado
        /// </summary>
        /// <param name="empresaID">ID da Empresa</param>
        [HttpGet]
        [Route("api/responsavel/getByEmpresaID/{empresaID}")]
        public IHttpActionResult GetByEmpresaID(int empresaID)
        {
            try
            {
                return Ok(_repository.GetByEmpresaID(empresaID));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Responsável relacionado ao ID informado
        /// </summary>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var responsavel = _repository.GetByID(id);

                if (responsavel == null)
                    return NotFound();

                return Ok(responsavel);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui um Responsável
        /// </summary>
        /// <param name="responsavel">Objeto do tipo Responsavel</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] Responsavel responsavel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _repository.Insert(responsavel);

                return Ok(responsavel);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de um Responsável
        /// </summary>
        /// <param name="responsavel">Objeto do tipo Responsavel</param>
        /// <param name="id">ID do Responsável que será atualizado</param>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Responsavel responsavel)
        {
            try
            {
                var responsavelDB = _repository.GetByID(id);

                if (responsavelDB == null)
                    return NotFound();

                responsavelDB.Cargo = responsavel.Cargo;
                responsavelDB.Matricula = responsavel.Matricula;
                responsavelDB.Nome = responsavel.Nome;
                responsavelDB.Usuario = null;
                responsavelDB.UsuarioID = responsavel.UsuarioID;

                _repository.Update(responsavelDB);

                return Ok(responsavelDB);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um Responsável
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var responsavel = _repository.GetByID(id);

                if (responsavel == null)
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
