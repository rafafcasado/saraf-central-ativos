using System.Web.Mvc;

namespace CentralAtivos.Web.Controllers
{
    public class LocalController : WebController
    {
        public virtual ActionResult Filiais()
        {
            return View();
        }
    }
}