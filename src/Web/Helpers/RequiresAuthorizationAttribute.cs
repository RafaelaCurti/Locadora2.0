using Locadora.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Locadora.Web.Helpers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class RequiresAuthorizationAttribute : ActionFilterAttribute, IAuthorizationFilter, IExceptionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var uri = filterContext.HttpContext.Request.Url.AbsoluteUri;
            SecurityContext.Do.Init(() => filterContext.HttpContext.User.Identity, uri.ToLower().Contains("painel"));

            if (SecurityContext.Do == null || !SecurityContext.Do.IsAuthenticated)
            {
                NotLogged(filterContext);
                return;
            }
            base.OnActionExecuting(filterContext);
        }
        private static void NotLogged(ActionExecutingContext filterContext)
        {
            var rUrl = "";
            if (!string.IsNullOrWhiteSpace(filterContext.HttpContext.Request.QueryString["returnUrl"]))
                rUrl = filterContext.HttpContext.Request.QueryString["returnUrl"].Replace("/Web_deploy", "");
            else
                rUrl = filterContext.HttpContext.Request.Url.AbsoluteUri.Replace("/Web_deploy", "");

            if (rUrl.ToLower().Contains("painel"))
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "Login" }, { "Area", "Painel" }, { "returnUrl", rUrl } });
            else
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "Login" }, { "Area", "Cliente" }, { "returnUrl", rUrl } });
        }

        private static void NotAuthorizated(ExceptionContext filterContext)
        {

        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
        }

        #region IExceptionFilter Members

        public void OnException(ExceptionContext exceptionContext)
        {
            if (exceptionContext.Exception is AuthorizationException)
                NotAuthorizated(exceptionContext);
        }

        #endregion
    }
}