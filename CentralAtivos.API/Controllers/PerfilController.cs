using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class PerfilController : ApiController
    {
        private readonly IPerfil _repository;
        private readonly IPerfilMenu _perfilMenuRepository;
        private readonly IPermissao _permissaoRepository;
        private readonly IRequisicao _requisicaoRepository;

        public PerfilController(IPerfil repository, IPermissao permissaoRepository, IRequisicao requisicaoRepository, IPerfilMenu perfilMenuRepository)
        {
            _repository = repository;
            _permissaoRepository = permissaoRepository;
            _requisicaoRepository = requisicaoRepository;
            _perfilMenuRepository = perfilMenuRepository;
        }

        /// <summary>
        /// Retorna Lista de Perfis Ativos Cadastrados
        /// </summary>
        [HttpGet]
        [Route("api/perfil/getPerfis")]
        public IHttpActionResult GetPerfis()
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
        /// Retorna Perfil relacionado ao ID informado
        /// </summary>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var perfil = _repository.GetByID(id);

                if (perfil == null)
                    return NotFound();

                return Ok(perfil);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui um Perfil
        /// </summary>
        /// <param name="perfil">Objeto do tipo Perfil</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] Perfil perfil)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Verificar campos obrigatórios");

                _repository.Insert(perfil);

                return Ok(perfil);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de um Perfil
        /// </summary>
        /// <remarks>
        /// É possível atualizar, apenas, o campo Nome
        /// </remarks>
        /// <param name="perfil">Objeto do tipo Perfil</param>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Perfil perfil)
        {
            try
            {
                var perfilDB = _repository.GetByID(id);

                if (perfilDB == null)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest("Verificar campos obrigatórios");

                perfilDB.Nome = perfil.Nome;

                _repository.Update(perfilDB);

                return Ok(perfilDB);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um Perfil
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var perfil = _repository.GetByID(id);

                if (perfil == null)
                    return NotFound();

                _repository.Delete(id);

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Lista de Permissões do Perfil
        /// </summary>
        [HttpGet]
        [Route("api/perfil/getPermissoes/{perfilID}")]
        public IHttpActionResult GetPermissoes(int perfilID)
        {
            try
            {
                var perfil = _repository.GetByID(perfilID);

                if (perfil == null)
                    return NotFound();

                var permissoesPerfil = _permissaoRepository.GetByPerfilID(perfil.ID);
                var requisicoes = _requisicaoRepository.GetAll();

                //var permissoes = from req in requisicoes
                //                 join perm in permissoesPerfil on req.ID equals perm.RequisicaoID into a
                //                 from j in a.DefaultIfEmpty()
                //                 select new
                //                 {
                //                     ID = (j == null ? (int?)null : j.ID),
                //                     Requisicao = req,
                //                     TemPermissao = (j == null ? false : true)
                //                 };

                //return Ok(permissoes);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
