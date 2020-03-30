using CentralAtivos.Domain.Interfaces;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CentralAtivos.API.Filters
{
    public class LogFilter : ActionFilterAttribute
    {
        public LogFilter()
        {
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string[] metodosLog = { "POST", "PUT", "DELETE" };

            var action = actionContext.ActionDescriptor.ActionName;
            var controller = actionContext.ControllerContext.ControllerDescriptor.ControllerName;

            if (metodosLog.Contains(actionContext.Request.Method.ToString()))
            {
                var logUsuarioRepository = (ILogUsuario)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILogUsuario));
                var requisicaoRepository = (IRequisicao)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IRequisicao));

                var context = (HttpContextBase)actionContext.Request.Properties["MS_HttpContext"];
                var identity = (System.Security.Claims.ClaimsIdentity)context.User.Identity;

                var userID = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == "UsuarioID").Value);

                var req = requisicaoRepository.Get(action, controller);

                logUsuarioRepository.Insert(new Domain.Entities.LogUsuario() { RequisicaoID = req.ID, UsuarioID = userID });
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {

        }
    }
}