using CentralAtivos.API.Filters;
using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    [Authorize, LogFilter]
    public class UsuarioController : ApiController
    {
        private readonly IUsuario _repository;

        public UsuarioController(IUsuario repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retorna Lista de Usuários Ativos Cadastrados
        /// </summary>
        [HttpGet]
        [Route("api/usuario/getUsuarios")]
        public IHttpActionResult GetUsuarios()
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
        /// Retorna Lista de Usuários Ativos Cadastrados de acordo com ID da Empresa enviada como parâmetro
        /// </summary>
        /// <param name="empresaID">ID da Empresa</param>
        [HttpGet]
        [Route("api/usuario/getByEmpresaID/{empresaID}")]
        public IHttpActionResult GetByEmpresaID(int empresaID)
        {
            try
            {
                List<Usuario> usuarios = null;

                if (empresaID == 0)
                    usuarios = _repository.GetAll().Where(x => x.EmpresaID == null && x.DataExclusao == null).ToList();
                else
                    usuarios = _repository.GetByEmpresaID(empresaID);

                return Ok(usuarios);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna Usuário relacionado ao ID informado
        /// </summary>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var user = _repository.GetByID(id);

                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inclui um Usuário
        /// </summary>
        /// <param name="usuario">Objeto do tipo Usuario</param>
        [HttpPost]
        public IHttpActionResult Post([FromBody] Usuario usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Verificar campos obrigatórios");

                if (_repository.EmailExists(usuario.Email))
                    return BadRequest("E-mail já utilizado por outro usuário");

                _repository.Insert(usuario);

                if (usuario.NotificarUsuario)
                {
                    string body = @"

                    <html>
                    <head>
                        <title></title>
                    </head>
                    <body>
                        <p><img src='{0}' width='200' /></p>                        
                        <p>Olá, <strong>{1}</strong>. Seja Bem Vindo(a)!</p>
                        <p>Você está cadastrad(a) na Central de Ativos e já pode acessar o sistema.</p>
                        <p>Para isso, acesse o link abaixo e cadastre sua senha.</p>                        
                        <p><a href='{2}'>Clique Aqui</a></p>
                        <p>Em caso de dúvidas, entre em contato conosco.</p>
                        <p><strong>Equipe Saraf</strong></p>
                    </body>
                    </html>      
                ";

                    body = string.Format(body, System.Configuration.ConfigurationManager.AppSettings["urlBaseWeb"] + "content/img/logo.png", usuario.Nome, System.Configuration.ConfigurationManager.AppSettings["urlBaseWeb"] + "home/primeiroacesso");

                    var email = new Helpers.Email(usuario.Email, "Central de Ativos - Seja Bem Vindo", body, true, "Central de Ativos", usuario.Nome);

                    email.Enviar();
                }

                return Ok(usuario);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de um Usuário
        /// </summary>
        /// <remarks>
        /// É possível alterar, apenas, os campos Nome e Email
        /// </remarks>
        /// <param name="usuario">Objeto do tipo Usuario</param>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Usuario usuario)
        {
            try
            {
                if (id != usuario.ID)
                    return NotFound();

                var usuarioDB = _repository.GetByID(id);

                if (usuarioDB == null)
                    return NotFound();

                if (string.IsNullOrEmpty(usuario.Nome) || string.IsNullOrEmpty(usuario.Email))
                    return BadRequest("Os campos Nome e Email são obrigatórios");

                usuarioDB.Email = usuario.Email;
                usuarioDB.Nome = usuario.Nome;
                usuarioDB.EmpresaID = usuario.EmpresaID;
                usuarioDB.Perfil = null;
                usuarioDB.PerfilID = usuario.PerfilID;

                usuarioDB.ConfirmacaoSenha = usuarioDB.Senha;

                _repository.Update(usuarioDB);

                return Ok(usuarioDB);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um Usuário
        /// </summary>
        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var usuario = _repository.GetByID(id);

                if (usuario == null)
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
        /// Notifica Primeiro Acesso do Usuário de acordo com ID informado
        /// </summary>
        [HttpGet, Route("api/usuario/notificarPrimeiroAcesso/{usuarioID}")]
        public IHttpActionResult NotificarPrimeiroAcesso(int usuarioID)
        {
            try
            {
                var usuario = _repository.GetByID(usuarioID);

                if (usuario == null)
                    return NotFound();

                if (!usuario.PrimeiroAcesso)
                    return BadRequest("O usuário informado já acessou o sistema, não é possível notificar Primeiro Acesso");

                string body = @"

                    <html>
                    <head>
                        <title></title>
                    </head>
                    <body>
                        <p><img src='{0}' width='200' /></p>                        
                        <p>Olá, <strong>{1}</strong>. Seja Bem Vindo(a)!</p>
                        <p>Você está cadastrado(a) na Central de Ativos e já pode acessar o sistema.</p>
                        <p>Para isso, acesse o link abaixo e cadastre sua senha.</p>                        
                        <p><a href='{2}'>Clique Aqui</a></p>
                        <p>Em caso de dúvidas, entre em contato conosco.</p>
                        <p><strong>Equipe Saraf</strong></p>
                    </body>
                    </html>      
                ";

                body = string.Format(body, System.Configuration.ConfigurationManager.AppSettings["urlBaseWeb"] + "content/img/logo.png", usuario.Nome, System.Configuration.ConfigurationManager.AppSettings["urlBaseWeb"] + "home/primeiroacesso");

                var email = new Helpers.Email(usuario.Email, "Central de Ativos - Seja Bem Vindo", body, true, "Central de Ativos", usuario.Nome);

                email.Enviar();

                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
