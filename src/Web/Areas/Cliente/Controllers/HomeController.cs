using Locadora.Domain;
using Locadora.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Locadora.Web.Areas.Cliente.Controllers
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
            var client = TClient.Authenticate(model);
            if (client != null)
            {
                FormsAuthentication.SetAuthCookie(model.Client, true);
                HttpContext.User = new GenericPrincipal(new GenericIdentity(model.Client), new string[] { });
                TempData["Alerta"] = new Alert("sucess", "Bem-vindo(a)");
                return RedirectToAction(MVC.Cliente.Home.Index());
            }
            else
            {
                //ViewBag.Alerta = new Alert("error", "Login ou senha inválida");
                TempData["Alerta"] = new Alert("error", "Senha ou Login Inválidos");
                return View(model);
            }
        }
        public virtual ActionResult Login()
        {
            return View();
        }
    }
}