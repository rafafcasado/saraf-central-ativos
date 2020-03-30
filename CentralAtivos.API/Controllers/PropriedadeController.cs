using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class PropriedadeController : ApiController
    {
        private readonly IPropriedade _repository;
        private readonly IPropriedadeValor _valoresRepository;
        private readonly IEspecie _especieRepository;
        private readonly IEspeciePropriedade _especiePropriedadeRepository;

        public PropriedadeController(IPropriedade repository, IPropriedadeValor valoresRepository, IEspecie especieRepository, IEspeciePropriedade especiePropriedadeRepository)
        {
            _repository = repository;
            _valoresRepository = valoresRepository;
            _especieRepository = especieRepository;
            _especiePropriedadeRepository = especiePropriedadeRepository;
        }

        /// <summary>
        /// Retorna Lista dos Propriedade Ativas Cadastradas para uma Empresa de acordo com o ID informado
        /// </summary>
        /// <param name="empresaID">ID da Empresa</param>
        [HttpGet]
        [Route("api/propriedade/getByEmpresaID/{empresaID}")]
        public IHttpActionResult GetByEmpresaID(int empresaID)
        {
            try
            {
                var propriedades = _repository.GetByEmpresaID(empresaID).ToList();

                return Ok(propriedades);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui uma Propriedade
        /// </summary>
        /// <param name="propriedade">Objeto do tipo Propriedade</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] Propriedade propriedade)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                List<PropriedadeValor> valores = propriedade.Valores.ToList();

                propriedade.Valores = null;

                _repository.Insert(propriedade);

                foreach (var valor in valores)
                {
                    valor.PropriedadeID = propriedade.ID;

                    _valoresRepository.Insert(valor);
                }

                if (propriedade.VincularEspecies)
                {
                    foreach (var esp in _especieRepository.GetByEmpresaID(propriedade.EmpresaID))
                    {
                        _especiePropriedadeRepository.Insert(new EspeciePropriedade { EspecieID = esp.ID, PropriedadeID = propriedade.ID });
                    }
                }

                return Ok(propriedade);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Propriedade relacionada ao ID informado
        /// </summary>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var propriedade = _repository.GetByID(id);

                if (propriedade == null)
                    return NotFound();

                return Ok(propriedade);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de uma Propriedade
        /// </summary>
        /// <param name="propriedade">Objeto do tipo Propriedade</param>
        /// <param name="id">ID da Propriedade que será atualizada</param>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Propriedade propriedade)
        {
            try
            {
                var propriedadeDB = _repository.GetByID(id);

                if (propriedadeDB == null)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                propriedadeDB.Nome = propriedade.Nome;
                propriedadeDB.Fixa = propriedade.Fixa;
                propriedadeDB.Valores = null;

                _repository.Update(propriedadeDB);

                List<PropriedadeValor> valores = propriedade.Valores.ToList();

                foreach (var valor in valores)
                {

                    if (valor.ID == 0)
                    {
                        valor.PropriedadeID = propriedade.ID;

                        _valoresRepository.Insert(valor);
                    }
                    else
                        _valoresRepository.Update(valor);
                }

                return Ok(propriedadeDB);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui uma Propriedade
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var propriedade = _repository.GetByID(id);

                if (propriedade == null)
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
