using Locadora.Domain;
using Locadora.Helpers;
using Locadora.Web.Areas.Painel.Controllers;
using Locadora.Web.Helpers;
using Microsoft.Ajax.Utilities;
using NHibernate.Mapping;
using NPOI.HSSF.Record;
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
    public partial class ClientesController : BaseController
    {
        public virtual ActionResult Login()
        {
            return View();
        }
        //public virtual ActionResult Editar()   
        //{
        //    var usuarioLogado = (TClient)ViewBag.Cliente;
        //    var preferenciasCliente = usuarioLogado.TPreferences.Select(x => x.Category.Id).ToList();
        //    ViewBag.MostraSenha = false;
        //    ViewBag.EnumProfileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());
        //    ViewBag.Category = TCategory.List(x => !preferenciasCliente.Contains(usuarioLogado.Id)).ToSelectList(x => x.Id, x => x.Name);
        //    ViewBag.Genero = TMovieCategory.ListAll().ToSelectList(x => x.Category.Id, x => x.Category.Name);
        //    return View(usuarioLogado);
        //}

        //[HttpPost]
        //public virtual ActionResult Editar(TClient clienteEditando)
        //{
        //    try
        //    {
        //        var usuarioLogado = (TClient)ViewBag.Cliente;
        //        usuarioLogado.Edit();
        //        TPreference.SavePreferences(usuarioLogado);
        //        TempData["Alerta"] = new Alert("success", "Seu perfil foi editado com sucesso");
        //        return RedirectToAction(MVC.Cliente.Home.Index());
        //    }
        //    catch (SimpleValidationException ex)
        //    {
        //        ViewBag.MostraSenha = false;
        //        ViewBag.EnumProfileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());
        //        ViewBag.Category = TCategory.ListAll().ToSelectList(x => x.Id, x => x.Name);
        //        return HandleViewException(clienteEditando, ex);
        //    }
        //}
        //public virtual ActionResult ListarPreferencia(int id)
        //{
        //    var preference = TCategory.Load(id);
        //    return PartialView("_listar-preferencias", preference);
        //}
        public virtual ActionResult Editar()
        {
            var usuarioLogado = (TClient)ViewBag.Cliente;
            ViewBag.MostraSenha = false;
            var preferenciasCliente = usuarioLogado.TPreferences.Select(x => x.Category.Id).ToList();
            ViewBag.EnumProfileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());
            ViewBag.Genero = TCategory.List( x=> !preferenciasCliente.Contains(usuarioLogado.Id)).ToSelectList(x => x.Id, x => x.Name);
            return View(usuarioLogado);
        }

        [HttpPost]
        public virtual ActionResult Editar(TClient model)
        {
            try
            {
                var usuarioLogado = (TClient)ViewBag.Cliente;
                //new TClient = TClient.Load(usuarioLogado);
                usuarioLogado.Name = model.Name;
                usuarioLogado.Email = model.Email;
                usuarioLogado.Telephone = model.Telephone;
                usuarioLogado.EnumProfileClient = model.EnumProfileClient;
                usuarioLogado.Preference = model.Preference;
                usuarioLogado.Update();
                TempData["Alerta"] = new Alert("success", "Cliente editado com sucesso");
                //TempData["Alerta"] = new Alert("success", "Seu cliente foi ceditado com sucesso");
                return RedirectToAction(MVC.Cliente.Home.Index());
            }
            catch (SimpleValidationException ex)
            {
                ViewBag.MostraSenha = false;
                ViewBag.EnumProfileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());
                ViewBag.Category = TCategory.ListAll().ToSelectList(x => x.Id, x => x.Name);
                TempData["Alerta"] = new Alert("error", "Cliente editado com sucesso");
                return HandleViewException(model, ex);
            }
        }
        public virtual ActionResult ListarPreferencia(int id)
        {
            var preference = TCategory.Load(id);
            return PartialView("_listar-preferencias", preference);
        }
        [HttpPost]
        public virtual ActionResult Login(object diff)
        {
            return View();
        }

        protected ActionResult HandleViewException<T>(T model, SimpleValidationException ex)
        {
            ModelState.Clear();
            foreach (var item in ex.Errors)
                ModelState.AddModelError(item.PropertyName, item.Message);
            return View(model);
        }
        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(MVC.Cliente.Home.Index());
        }
    }
}