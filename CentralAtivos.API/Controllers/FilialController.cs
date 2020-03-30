using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class FilialController : ApiController
    {
        private readonly IFilial _repository;

        public FilialController(IFilial repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retorna Lista de Filiais Ativas Cadastradas para a Empresa de acordo com o ID informado
        /// </summary>
        /// <param name="empresaID">ID da Empresa</param>
        [HttpGet]
        [Route("api/filial/getByEmpresaID/{empresaID}")]
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
        /// Retorna Filial relacionada ao ID informado
        /// </summary>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var filial = _repository.GetByID(id);

                if (filial == null)
                    return NotFound();

                return Ok(filial);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui uma Filial
        /// </summary>
        /// <param name="filial">Objeto do tipo Filial</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] Filial filial)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _repository.Insert(filial);

                return Ok(filial);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de uma Filial
        /// </summary>
        /// <param name="filial">Objeto do tipo Filial</param>
        /// <param name="id">ID da Filial que será atualizada</param>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Filial filial)
        {
            try
            {
                var filialDB = _repository.GetByID(id);

                if (filialDB == null)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                filialDB.Bairro = filial.Bairro;
                filialDB.CEP = filial.CEP;
                filialDB.Cidade = filial.Cidade;
                filialDB.Codigo = filial.Codigo;
                filialDB.CNPJ = filial.CNPJ;
                filialDB.Complemento = filial.Complemento;
                filialDB.Endereco = filial.Endereco;
                filialDB.Nome = filial.Nome;
                filialDB.Numero = filial.Numero;
                filialDB.UF = filial.UF;

                _repository.Update(filialDB);

                return Ok(filialDB);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui uma Filial
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var filial = _repository.GetByID(id);

                if (filial == null)
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
