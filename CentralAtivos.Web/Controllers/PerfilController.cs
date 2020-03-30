using System.Web.Mvc;

namespace CentralAtivos.Web.Controllers
{
    public class PerfilController : WebController
    {
        public virtual ActionResult Permissoes(long id)
        {
            return View();
        }
    }
}