using System.Web.Mvc;

namespace CentralAtivos.Web.Controllers
{
    public class InventarioController : WebController
    {
        public ActionResult Configuracao()
        {
            return View();
        }
    }
}