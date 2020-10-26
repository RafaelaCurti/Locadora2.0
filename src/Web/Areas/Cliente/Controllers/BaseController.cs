using Locadora.Domain;
using Locadora.Helpers;
using Locadora.Web.Helpers;
using Simple.Validation;
using Simple.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Locadora.Web.Areas.Cliente.Controllers
{
    public partial class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
        }
        protected ActionResult HandleViewException<T>(T model, SimpleValidationException ex)
        {
            ModelState.Clear();
            foreach (var item in ex.Errors)
                ModelState.AddModelError(item.PropertyName, item.Message);
            return View(model);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Alerta = TempData["Alerta"];
            var username = TClient.FindByUsername(User.Identity.Name);
            if (User.Identity.IsAuthenticated && username != null)
                ViewBag.Cliente = username;
            else
                FormsAuthentication.SignOut();
        }
    }
}