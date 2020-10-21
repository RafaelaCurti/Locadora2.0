using Locadora.Domain;
using Locadora.Web.Helpers;
using Simple.Validation;
using Simple.Web.Mvc;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Locadora.Web.Areas.Painel.Controllers
{
    public partial class UsuariosController : BaseController
    {
        [RequiresAuthorization]
        public virtual ActionResult Index()
        {
            var usuarios = TUser.ListAll().ToList();
            return View(usuarios);
        }

        public virtual ActionResult Cadastrar()
        {
            var usuarios = new TUser();
            ViewBag.MostraSenha = true;
            ViewBag.EnumProfileUser = EnumHelper.ListAll<ProfileUser>().ToSelectList(x => x, x => x.Description());
            return View(usuarios);
        }

        [HttpPost]
        public virtual ActionResult Cadastrar(TUser model)
        {
            try   
            {
                model.Password = TUser.HashPassword(model.PasswordString);
                model.Save();
                //TempData["Alerta"] = new Alert("success", "Seu usuário foi cadastrado com sucessor");
                TempData["Alerta"] = new Alert("success", "Usuário cadastrado com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                ViewBag.MostraSenha = true;
                ViewBag.EnumProfileUser = EnumHelper.ListAll<ProfileUser>().ToSelectList(x => x, x => x.Description());
                //TempData["Alerta"] = new Alert("error", "Seu usuário não foi cadastrado");
                TempData["Alerta"] = new Alert("error", "Usuário não pode ser cadastrado com sucesso");
                return HandleViewException(model, ex);
            }
        }
        public virtual ActionResult Editar(int id)
        {
            var usuarios = TUser.Load(id);
            ViewBag.MostraSenha = false;
            ViewBag.EnumProfileUser = EnumHelper.ListAll<ProfileUser>().ToSelectList(x => x, x => x.Description());
            return View(usuarios);
        }

        [HttpPost]
        public virtual ActionResult Editar(TUser model)
        {
            try
            {
                model.Edit();
                //TempData["Alerta"] = new Alert("success", "Seu usuário foi editado com sucesso");
                TempData["Alerta"] = new Alert("success", "Usuário editado com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                ViewBag.MostraSenha = false;
                ViewBag.EnumProfileUser = EnumHelper.ListAll<ProfileUser>().ToSelectList(x => x, x => x.Description());
                //TempData["Alerta"] = new Alert("error", "Seu usuário não foi editado");
                TempData["Alerta"] = new Alert("error", "Usuário não pode ser editado");
                return HandleViewException(model, ex);
            }
        }
        public virtual ActionResult Excluir(int id)
        {
            var usuarios = TUser.Load(id);
            return PartialView("_excluir", usuarios);
        }

        [HttpPost]
        public virtual ActionResult Excluir(int id, object diff)
        {
            TUser.Delete(id);
            return RedirectToAction("Index");
        }


        public virtual ActionResult Login()
        {
            //var usuarios = TUser.Load(id);
            return View();
        }

        [HttpPost]
        public virtual ActionResult Login(string user, string password)
        {
            try
            {
                // código para verificar se o usuário existe e se a senha está correta!
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                // caso dê um erro fazer o que estiver aqui!
                return RedirectToAction("Login");
                throw;
            }
        }

        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(MVC.Painel.Home.Index());
        }

    }
}