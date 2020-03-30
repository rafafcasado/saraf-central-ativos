using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class RequisicaoController : ApiController
    {
        private readonly IFuncionalidade _funcionalidadeRepository;
        private readonly IRequisicao _repository;
        private readonly IPerfil _perfilRepository;
        private readonly IPermissao _permissaoRepository;

        public RequisicaoController(IFuncionalidade funcionalidadeRepository, IRequisicao repository, IPerfil perfilRepository, IPermissao permissaoRepository)
        {
            _funcionalidadeRepository = funcionalidadeRepository;
            _repository = repository;
            _perfilRepository = perfilRepository;
            _permissaoRepository = permissaoRepository;
        }

        /// <summary>
        /// Retorna Lista de Requisições Cadastradas
        /// </summary>
        [HttpGet]
        [Route("api/requisicao/getRequisicoes/{perfilID}")]
        public IHttpActionResult GetRequisicoes(int perfilID)
        {
            try
            {
                var perfil = _perfilRepository.GetByID(perfilID);

                if (perfil == null)
                    return BadRequest("Perfil não localizado");

                var permissoes = _permissaoRepository.GetByPerfilID(perfilID);

                var filter = new List<object>();

                foreach (var e in _funcionalidadeRepository.GetAll())
                {
                    var permissao = permissoes.Where(x => x.FuncionalidadeID == e.ID).SingleOrDefault();

                    filter.Add(new { funcionalidade = e.Nome, metodos = "GET,POST,PUT,DELETE", metodosPermitidos = permissao == null ? string.Empty : permissao.Metodos });
                }

                return Ok(filter);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
