using System.Web.Mvc;

namespace CentralAtivos.Web.Controllers
{
    public class UsuarioController : WebController
    {
        public virtual ActionResult RecadastrarSenha(string token)
        {
            return View();
        }
    }
}