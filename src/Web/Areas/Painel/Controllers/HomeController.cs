using Locadora.Domain;
using Locadora.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Locadora.Web.Areas.Painel.Controllers
{
    public partial class HomeController : BaseController
    {
        [RequiresAuthorization]
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Login(Login model)
        {
            var usuario = TUser.Authenticate(model);
            if (usuario != null)
            {
                FormsAuthentication.SetAuthCookie(model.Username, true);
                HttpContext.User = new GenericPrincipal(new GenericIdentity(model.Username), new string[] { });
                TempData["Alerta"] = new Alert("sucess", "Bem-vindo(a)");
                //ViewBag.Alerta = new Alert("sucess", "Bem-vindo(a)");
                return RedirectToAction(MVC.Painel.Home.Index());
            }
            else
            {
                ViewBag.Alerta = new Alert("error", "Senha ou Login inválidos!");
                return View(model);
            }

        }
        public virtual ActionResult Login()
        {
            return View();
        }


    }
}