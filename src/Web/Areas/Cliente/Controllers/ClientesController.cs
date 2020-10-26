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


    public partial class ClientesController : BaseController
    {
        public virtual ActionResult Login()
        {
            return View();
        }

        public virtual ActionResult Cadastrar()
        {
            var cliente = new TClient();
            ViewBag.MostraSenha = true;
            ViewBag.EnumProfileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());
            ViewBag.Genero = TCategory.ListAll().ToSelectList(x => x.Id, x => x.Name);
            return View(cliente);
        }

        [HttpPost]
        public virtual ActionResult Cadastrar(TClient model)
        {
            try
            {
                model.Password = TClient.HashPassword(model.PasswordString);
                model.Save();
                TPreference.SavePreferences(model);
                TempData["Alerta"] = new Alert("success", "Cliente editado com sucesso");
                return RedirectToAction(MVC.Cliente.Home.Login());
            }
            catch (SimpleValidationException ex)
            {
                ViewBag.MostraSenha = true;
                ViewBag.EnumProfileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());
                TempData["Alerta"] = new Alert("error", "Você não pode ser cadastrado");
                return HandleViewException(model, ex);
            }

        }
        public virtual ActionResult ListarPreferencia(int id)
        {
            var preference = TCategory.Load(id);
            return PartialView("_listar-preferencias", preference);
        }

        [RequiresAuthorization]
        public virtual ActionResult Editar()
        {
            var usuarioLogado = (TClient)ViewBag.Cliente;
            ViewBag.MostraSenha = false;
            var preferenciasCliente = usuarioLogado.TPreferences.Select(x => x.Category.Id).ToList();
            ViewBag.EnumProfileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());
            ViewBag.Genero = TCategory.List(x => !preferenciasCliente.Contains(usuarioLogado.Id)).ToSelectList(x => x.Id, x => x.Name);
            return View(usuarioLogado);
        }

        [HttpPost]
        public virtual ActionResult Editar(TClient model)
        {
            try
            {
                var usuarioLogado = (TClient)ViewBag.Cliente;
                model.Id = usuarioLogado.Id;
                var cliente = TClient.Load(usuarioLogado.Id);
                cliente.Name = model.Name;
                cliente.Email = model.Email;
                cliente.Telephone = model.Telephone;
                cliente.EnumProfileClient = model.EnumProfileClient;
                cliente.Preference = model.Preference;
                cliente.Update();
                TPreference.SavePreferences(model);
                TempData["Alerta"] = new Alert("success", "Cliente editado com sucesso");
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