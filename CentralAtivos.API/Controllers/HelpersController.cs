using CentralAtivos.Domain.Entities;
using CentralAtivos.Domain.Interfaces;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CentralAtivos.API.Controllers
{
    public class HelpersController : ApiController
    {
        private readonly IUsuario _repository;
        private readonly IResponsavel _responsavelRepository;

        public HelpersController(IUsuario repository, IResponsavel responsavelRepository)
        {
            _repository = repository;
            _responsavelRepository = responsavelRepository;
        }

        [HttpGet, Authorize, Route("api/helpers/getUsuarioLogado")]
        public IHttpActionResult GetUsuarioLogado()
        {
            try
            {
                var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.Current.User.Identity;

                var userID = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == "UsuarioID").Value);

                var user = _repository.GetByID(userID);

                if (user == null)
                    return NotFound();

                var usuarioRetorno = new {
                    user.Empresa,
                    user.Perfil,
                    user.EmpresaID,
                    user.PerfilID,
                    user.Matricula,
                    user.Nome,
                    user.Email,
                    user.Senha,
                    user.PrimeiroAcesso,
                    user.TokenSolicitacaoSenha,
                    user.DataValidadeTokenSenha,
                    user.ConfirmacaoSenha,
                    user.NotificarUsuario,
                    user.ID,
                    user.DataCadastroFormatada,
                    Responsavel = _responsavelRepository.GetByUsuarioID(user.ID)
                };

                return Ok(usuarioRetorno);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult PrimeiroAcesso([FromBody] Usuario usuario)
        {
            try
            {
                var usuarioDB = _repository.GetAll().Where(x => x.Email == usuario.Email).SingleOrDefault();

                if (usuarioDB == null)
                    return NotFound();

                if (!usuarioDB.PrimeiroAcesso)
                    return BadRequest("Não é o primeiro acesso desse usuário. Acesse Esqueci Minha Senha para recadastrar uma nova.");

                usuarioDB.Senha = Helpers.Criptografia.Cripto(usuario.Senha);
                usuarioDB.ConfirmacaoSenha = Helpers.Criptografia.Cripto(usuario.Senha);
                usuarioDB.PrimeiroAcesso = false;

                _repository.Update(usuarioDB);

                return Ok("Primeiro Acesso executado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

                throw;
            }
        }

        [HttpPost, Route("api/helpers/solicitarRecadastramentoSenha"), AllowAnonymous]
        public IHttpActionResult SolicitarRecadastramentoSenha([FromBody] Usuario usuario)
        {
            try
            {
                usuario = _repository.GetAll().Where(x => x.Email.ToLower() == usuario.Email.ToLower() && x.DataExclusao == null).SingleOrDefault();

                if (usuario == null)
                    return NotFound();

                usuario.TokenSolicitacaoSenha = Guid.NewGuid();
                usuario.DataValidadeTokenSenha = DateTime.Now.AddDays(1);
                usuario.Perfil = null;
                usuario.ConfirmacaoSenha = usuario.Senha;

                _repository.Update(usuario);

                string body = @"

                    <html>
                    <head>
                        <title></title>
                    </head>
                    <body>
                        <p><img src='{0}' width='200' /></p>                        
                        <p>Olá, {1}!</p>
                        <p>Para recadastrar sua senha, clique no link abaixo:</p>
                        <p><a href='{2}'>Clique Aqui</a></p>
                        <p><strong>Equipe Saraf</strong></p>
                    </body>
                    </html>      
                ";

                body = string.Format(body, System.Configuration.ConfigurationManager.AppSettings["urlBaseWeb"] + "content/img/logo.png", usuario.Nome, System.Configuration.ConfigurationManager.AppSettings["urlBaseWeb"] + "usuario/recadastrarsenha/" + usuario.TokenSolicitacaoSenha);

                var email = new Helpers.Email(usuario.Email, "Central de Ativos - Recadastramento de Senha", body, true, "Central de Ativos", usuario.Nome);

                email.Enviar();

                return Ok(usuario);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("api/helpers/getUsuarioByTokenSenha/{token}")]
        public IHttpActionResult GetUsuarioByTokenSenha(Guid token)
        {
            try
            {
                var user = _repository.GetAll().Where(x => x.TokenSolicitacaoSenha == token).SingleOrDefault();

                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Route("api/helpers/novaSenha")]
        public IHttpActionResult NovaSenha([FromBody] Usuario usuario)
        {
            try
            {
                var usuarioDB = _repository.GetAll().Where(x => x.TokenSolicitacaoSenha == usuario.TokenSolicitacaoSenha).SingleOrDefault();

                if (usuarioDB == null)
                    return NotFound();

                if (usuarioDB.DataValidadeTokenSenha < DateTime.Now)
                    return BadRequest("Token de Recuração de Senha Expirado. Faça uma nova solicitação.");

                usuarioDB.Senha = Helpers.Criptografia.Cripto(usuario.Senha);
                usuarioDB.ConfirmacaoSenha = Helpers.Criptografia.Cripto(usuario.Senha);
                usuarioDB.TokenSolicitacaoSenha = null;
                usuarioDB.DataValidadeTokenSenha = null;

                _repository.Update(usuarioDB);

                return Ok("Senha recadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

                throw;
            }
        }
    }
}
