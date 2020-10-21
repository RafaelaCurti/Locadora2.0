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

namespace Locadora.Web.Areas.Painel.Controllers
{
    public partial class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {

        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //    ViewBag.Alerta = TempData["Alerta"];
            ViewBag.Alerta = TempData["Alerta"];
            var username = TUser.FindByUsername(User.Identity.Name);
            if (User.Identity.IsAuthenticated && username != null)
                ViewBag.Usuario = username;
            
            else
                
                FormsAuthentication.SignOut();
                
        }

        protected ActionResult HandleViewException<T>(T model, SimpleValidationException ex)
        {
            ModelState.Clear();
            foreach (var item in ex.Errors)
                ModelState.AddModelError(item.PropertyName, item.Message);
            return View(model);
        }
    }
}