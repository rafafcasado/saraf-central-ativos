using CentralAtivos.Domain.Interfaces;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CentralAtivos.API.Filters
{
    public class PermissaoFilter : ActionFilterAttribute
    {
        public PermissaoFilter()
        {
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var permissaoRepository = (IPermissao)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IPermissao));
            var requisicaoRepository = (IRequisicao)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IRequisicao));
            var usuarioRepository = (IUsuario)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUsuario));

            var context = (HttpContextBase)actionContext.Request.Properties["MS_HttpContext"];
            var identity = (System.Security.Claims.ClaimsIdentity)context.User.Identity;

            var userID = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == "UsuarioID").Value);
            var action = actionContext.ActionDescriptor.ActionName;
            var controller = actionContext.ControllerContext.ControllerDescriptor.ControllerName;

            var user = usuarioRepository.GetByID(userID);

            if (user == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));

            var permissions = permissaoRepository.GetByPerfilID(user.PerfilID);

            var req = requisicaoRepository.Get(action, controller);

            //if (!permissions.Select(x => x.RequisicaoID).Contains(req.ID))
            //    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));

        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {

        }
    }
}