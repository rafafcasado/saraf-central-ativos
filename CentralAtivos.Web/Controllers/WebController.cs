using System.Web.Mvc;

namespace CentralAtivos.Web.Controllers
{
    public abstract class WebController : Controller
    {
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult Novo()
        {
            return View("Model");
        }

        public virtual ActionResult Editar(long id)
        {
            return View("Model");
        }
    }
}