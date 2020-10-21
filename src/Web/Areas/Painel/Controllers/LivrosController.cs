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

namespace Locadora.Web.Areas.Painel.Controllers
{
    [RequiresAuthorization]
    public partial class LivrosController : BaseController
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
                //ViewBag.Alerta = new Alert("error", "Livro ou senha inválida");

                return View(model);
            }
        }

        public virtual ActionResult Index()
        {
            var livro = TBook.ListAll().ToList();
            return View(livro);
        }
        public virtual ActionResult Cadastrar()
        {
            var livro = new TBook();
            return View(livro);
        }

        [HttpPost]
        public virtual ActionResult Cadastrar(TBook model)
        {
            try
            {
                model.Save();
                //TempData["Alert"] = new Alert("success", "Seu livro foi cadastrado com sucesso");
                TempData["Alerta"] = new Alert("success", "Livro cadastrado com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                //TempData["Alert"] = new Alert("error", "Seu livro não foi cadastrado");
                TempData["Alerta"] = new Alert("error", "Livro não pode ser cadastrado com sucesso");
                return HandleViewException(model, ex);
            }
        }
        public virtual ActionResult Editar(int id)
        {
            var livro = TBook.Load(id);
            return View(livro);
        }

        [HttpPost]
        public virtual ActionResult Editar(TBook model)
        {
            try
            {
                model.Update();
                //TempData["Alert"] = new Alert("success", "Seu livro foi editado com sucesso");
                TempData["Alerta"] = new Alert("success", "Livro editado com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                //TempData["Alert"] = new Alert("error", "Seu livro não foi editado");
                TempData["Alerta"] = new Alert("success", "Livro não pode ser cadastrado com sucesso");
                return HandleViewException(model, ex);
            }
        }

        public virtual ActionResult Excluir(int id)
        {
            var book = TBook.Load(id);
            return PartialView("_excluir", book);
        }

        [HttpPost]
        public virtual ActionResult Excluir(int id, object diff)
        {
            TBook.Delete(id);
            return RedirectToAction("Index");
        }
        //protected ActionResult HandleViewException<T>(T model, SimpleValidationException ex)
        //{
        //    ModelState.Clear();
        //    foreach (var item in ex.Errors)
        //        ModelState.AddModelError(item.PropertyName, item.Message);
        //    return View(model);
        //}
    }
}