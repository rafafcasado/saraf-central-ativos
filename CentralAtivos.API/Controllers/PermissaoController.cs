using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class PermissaoController : ApiController
    {
        private readonly IPerfil _perfilRepository;
        private readonly IPermissao _permissaoRepository;
        private readonly IUsuario _usuarioRepository;

        public PermissaoController(IPerfil perfilRepository, IPermissao permissaoRepository, IUsuario usuarioRepository)
        {
            _perfilRepository = perfilRepository;
            _permissaoRepository = permissaoRepository;
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Retorna Permissões relacionadas ao ID do Perfil informado
        /// </summary>
        [HttpGet]
        [Route("api/permissao/{PerfilID}")]
        public IHttpActionResult Get(int PerfilID)
        {
            try
            {
                var perfil = _perfilRepository.GetByID(PerfilID);

                if (perfil == null)
                    return NotFound();

                var permissoes = _permissaoRepository.GetByPerfilID(PerfilID);

                //return Ok(permissoes.Select(x => new { x.ID, x.PerfilID, x.RequisicaoID }));
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Permissões do Usuário do Token de acordo com a Funcionalidade
        /// </summary>
        [HttpGet]
        [Route("api/permissao/getByFuncionalidade/{funcionalidade}")]
        public IHttpActionResult GetByFuncionalidade(string funcionalidade)
        {
            try
            {
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

                var usuarioID = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == "UsuarioID").Value);

                var usuario = _usuarioRepository.GetByID(usuarioID);

                var permissoes = _permissaoRepository.GetByPerfilID(usuario.PerfilID);

                var permissaoFuncionalidade = permissoes.Where(x => x.Funcionalidade.Nome.ToLower() == funcionalidade.ToLower()).SingleOrDefault();

                if (permissaoFuncionalidade == null)
                    return NotFound();
                else
                    return Ok(permissaoFuncionalidade.Metodos);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui uma Permissão
        /// </summary>
        /// <param name="permissao">Objeto do tipo Permissao</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] Permissao permissao)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _permissaoRepository.Insert(permissao);

                //return Ok(new { permissao.ID, permissao.PerfilID, permissao.RequisicaoID });
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza Permissão do Perfil
        /// </summary>
        /// <param name="permissao">Objeto do Tipo PermissaoAtualizacao</param>
        [HttpPut]
        public IHttpActionResult Put([FromBody] PermissaoAtualizacao permissao)
        {
            try
            {
                _permissaoRepository.Update(permissao.PerfilID, permissao.Funcionalidade, permissao.Metodo);

                return Ok("Permissão atualizada com Sucesso");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui uma Permissão
        /// </summary>
        [HttpDelete]
        [Route("api/permissao/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _permissaoRepository.Delete(id);

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class PermissaoAtualizacao
        {
            public int PerfilID { get; set; }
            public string Funcionalidade { get; set; }
            public string Metodo { get; set; }
        }
    }
}
