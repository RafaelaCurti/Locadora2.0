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
    public partial class UtenteController : BaseController
    {
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
                //TempData["Alert"] = new Alert("success", "Seu cliente foi cadastrado com sucesso");
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
    }
}