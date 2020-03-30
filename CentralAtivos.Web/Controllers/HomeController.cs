using System.Web.Mvc;

namespace CentralAtivos.Web.Controllers
{
    public class HomeController : WebController
    {
        [AllowAnonymous]
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("PrimeiroAcesso")]
        public ActionResult PrimeiroAcesso()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("EsqueciSenha")]
        public ActionResult EsqueciSenha()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }
    }
}