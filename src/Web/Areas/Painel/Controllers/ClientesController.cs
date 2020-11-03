using Locadora.Domain;
using Locadora.Web.Areas.ViewModels;
using Locadora.Web.Helpers;
using Simple.Entities;
using Simple.Validation;
using Simple.Web.Mvc;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;

namespace Locadora.Web.Areas.Painel.Controllers
{
    [RequiresAuthorization]
    public partial class ClientesController : BaseController
    {
       public virtual ActionResult Index()
        {
            ViewBag.Pagina = 1;
            ViewBag.MostraSenha = false;

            var usuario = (TUser)ViewBag.Usuario;
            var buscar = new ClientSearchViewModel().ConvertToClientSearch();
            var clientes = TClient.Search(buscar);
            var total = TClient.CountSearch(buscar);
            var page = new Page<TClient>(clientes, total);
            return View(page);
        }
        public virtual ActionResult Buscar(ClientSearchViewModel model)
        {
            model.pagina = model.pagina ?? 1;
            ViewBag.Pagina = model.pagina;
            // Preciso ver onde pega essa ViewBaf
            var buscar = model.ConvertToClientSearch();
            var clientes = TClient.Search(buscar);
            var total = TClient.CountSearch(buscar);
            ViewBag.EnumProfileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());
            ViewBag.Genero = TCategory.ListAll().ToSelectList(x => x.Id, x => x.Name);
            var page = new Page<TClient>(clientes, total);
            return PartialView("_buscar", page);
        }

        [HttpPost]
        public virtual ActionResult Login(Login model)
        {
            var usuario = TClient.Authenticate(model);
            if (usuario != null)
            {
                FormsAuthentication.SetAuthCookie(model.Username, true);
                HttpContext.User = new GenericPrincipal(new GenericIdentity(model.Username), new string[] { });
                //ViewBag.Alerta = new Alert("sucess", "Seja bem vindo(a)");
                return RedirectToAction(MVC.Painel.Clientes.Index());
            }
            else
            {
                //ViewBag.Alerta = new Alert("error", "Usuário ou senha inválida");
                return View(model);
            }
        }
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
                //TempData["Alert"] = new Alert("success", "Seu cliente foi cadastrado com sucesso");
                TempData["Alerta"] = new Alert("success", "Cliente cadastrado com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                ViewBag.MostraSenha = true;
                ViewBag.EnumProfileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());
                //TempData["Alert"] = new Alert("error", "Seu cliente não foi cadastrado");
                TempData["Alerta"] = new Alert("error", "Cliente não pode ser cadastrado");
                return HandleViewException(model, ex);
            }
        }
        public virtual ActionResult Editar(int id)
        {
            var cliente = TClient.Load(id);
            ViewBag.MostraSenha = false;
            ViewBag.EnumProfileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());
            ViewBag.Genero = TCategory.ListAll().ToSelectList(x => x.Id, x => x.Name);
            return View(cliente);
        }
        [HttpPost]
        public virtual ActionResult Editar(TClient model)
        {
            try
            {
                model.Edit();
                TPreference.SavePreferences(model);
                //TempData["Alerta"] = new Alert("success", "Seu cliente foi ceditado com sucesso");
                TempData["Alerta"] = new Alert("success", "Cliente editado com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                ViewBag.MostraSenha = false;
                ViewBag.EnumProfileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());
                //TempData["Alert"] = new Alert("error", "Seu cliente não foi cadastrado");
                TempData["Alerta"] = new Alert("success", "O cliente não pode ser editado com sucesso");
                return HandleViewException(model, ex);
            }
        }
        public virtual ActionResult ListarPreferencia(int id)
        {
            var preference = TCategory.Load(id);
            return PartialView("_listar-preferencias", preference);
        }
        public virtual ActionResult Excluir(int id)
        {
            var cliente = TClient.Load(id);
            return PartialView("_excluir", cliente);
        }

        [HttpPost]
        public virtual ActionResult Excluir(int id, object diff)
        {
            TPreference.Delete(x => x.Client.Id == id);
            TClient.Delete(id);
            return RedirectToAction("Index");
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
    }
}
