using Locadora.Domain;
using Locadora.Web.Helpers;
using Simple.Validation;
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
            List<TMovie> filmes;
            var client = (TClient)ViewBag.Cliente;
            var preferencias = client.TPreferences.Select(x => x.Category.Id).ToList();

            if (preferencias.Count != 0)
            {
                filmes = TMovieCategory.List(x => preferencias.Contains(x.Category.Id)).Select(x => x.Movie).ToList();
            }
            else
            {
                filmes = TMovie.ListAll().ToList();
            }
            return View(filmes);
        }
        public virtual ActionResult ListarGenero(int id)
        {
            var genero = TCategory.Load(id);
            return PartialView("_listar-genero", genero);
        }
        protected ActionResult HandleViewException<T>(T model, SimpleValidationException ex)
        {
            ModelState.Clear();
            foreach (var item in ex.Errors)
                ModelState.AddModelError(item.PropertyName, item.Message);
            return View(model);
        }
        public virtual ActionResult Login()
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
                TempData["Alerta"] = new Alert("success", "Bem-vindo(a)");
                return RedirectToAction(MVC.Cliente.Home.Index());
            }
            else
            {
                ViewBag.Alerta = new Alert("error", "Login ou senha inválida");
                return View(model);
            }
        }

    }
}
//namespace Locadora.Web.Areas.Cliente.Controllers
//{
//    public partial class HomeController : BaseController
//    {
//        [RequiresAuthorization]
//        public virtual ActionResult Index()
//        {
//            var filmes = TMovie.ListAll().ToList();
//            return View(filmes);
//        }
//        public virtual ActionResult ListarGenero(int id)
//        {
//            var genero = TCategory.Load(id);
//            return PartialView("_listar-genero", genero);
//        }
//        protected ActionResult HandleViewException<T>(T model, SimpleValidationException ex)
//        {
//            ModelState.Clear();
//            foreach (var item in ex.Errors)
//                ModelState.AddModelError(item.PropertyName, item.Message);
//            return View(model);
//        }
//        public virtual ActionResult Login()
//        {
//            return View();
//        }
//        [HttpPost]
//        public virtual ActionResult Login(Login model)
//        {
//            var client = TClient.Authenticate(model);
//            if (client != null)
//            {
//                FormsAuthentication.SetAuthCookie(model.Client, true);
//                HttpContext.User = new GenericPrincipal(new GenericIdentity(model.Client), new string[] { });
//                TempData["Alerta"] = new Alert("success", "Bem-vindo(a)");
//                return RedirectToAction(MVC.Cliente.Home.Index());
//            }
//            else
//            {
//                //ViewBag.Alerta = new Alert("error", "Login ou senha inválida");
//                TempData["Alerta"] = new Alert("error", "Senha ou Login Inválidos");
//                return View(model);
//            }
//        }

//    }
//}