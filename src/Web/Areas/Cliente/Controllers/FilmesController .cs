using Locadora.Domain;
using Locadora.Helpers;
using Locadora.Web.Helpers;
using Simple.Validation;
using Simple.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Locadora.Web.Areas.Cliente.Controllers
{
    [RequiresAuthorization]
    public partial class FilmesController : BaseController
    {
        [HttpPost]
        public virtual ActionResult Login(Login model)
        {
            var usuario = TClient.Authenticate(model);
            if (usuario != null)
            {
                FormsAuthentication.SetAuthCookie(model.Client, true);
                HttpContext.User = new GenericPrincipal(new GenericIdentity(model.Client), new string[] { });
                return RedirectToAction(MVC.Home.Index());
            }
            else
            {
                //ViewBag.Alerta = new Alert("error", "Usuário ou senha inválida");
                return View(model);
            }
        }
        public virtual ActionResult Index()
        //{
        //ViewBag.Genero = TMovieCategory.ListAll().ToSelectList(x => x.Category.Id, x => x.Category.Name);
        //var model = ViewBag.Genero;
        //var filmes = TMovie.ListAll();
        //return View(filmes);

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

        //}
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
    }
}