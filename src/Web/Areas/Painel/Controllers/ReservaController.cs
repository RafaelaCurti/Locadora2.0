using Locadora.Domain;
using Locadora.Web.Helpers;
using Simple.Validation;
using Simple.Web.Mvc;
using System.Linq;
using System.Web.Mvc;

namespace Locadora.Web.Areas.Painel.Controllers
{
    [RequiresAuthorization]
    public partial class ReservasController : BaseController
    {
        public virtual ActionResult Index()
        {
            var reserva = TReservation.ListAll().ToList();
            return View(reserva);
        }

        public virtual ActionResult Cadastrar()
        {
            var reserva = new TReservation();
            ViewBag.Client = TClient.ListAll().ToSelectList(x => x.Id, x => x.Name);
            ViewBag.Itens = TMovie.ListAll().ToSelectList(x => x.Id, x => x.Name);
            return View(reserva);
        }

        [HttpPost]
        public virtual ActionResult Cadastrar(TReservation model)
        {
            try
            {
                model.Save();
                TIten.SaveIten(model);
                //TempData["Alert"] = new Alert("success", "Sua reserva foi cadastrada com sucesso");
                TempData["Alerta"] = new Alert("success", "Reserva cadastrada com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                //TempData["Alert"] = new Alert("error", "Sua reserva não foi cadastrada");
                TempData["Alerta"] = new Alert("error", "Reserva não pode ser cadastrada com sucesso");
                return HandleViewException(model, ex);
            }
        }

        public virtual ActionResult Editar(int id)
        {
            var reserva = TReservation.Load(id);
            ViewBag.Client = TClient.ListAll().ToSelectList(x => x.Id, x => x.Name);
            ViewBag.Itens = TMovie.ListAll().ToSelectList(x => x.Id, x => x.Name);
            ViewBag.Sale = TSale.ListAll().ToSelectList(x => x.Id, x => x.EnumStatusSale);
            return View(reserva);
        }

        [HttpPost]
        public virtual ActionResult Editar(TReservation model)
        {
            try
            {
                model.Update();
                TIten.SaveIten(model);
                //TempData["Alert"] = new Alert("success", "Sua reserva foi editada com sucesso");
                TempData["Alerta"] = new Alert("success", "Reserva editada com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                //TempData["Alert"] = new Alert("error", "Sua reserva não foi editada");
                TempData["Alerta"] = new Alert("error", "Reserva não pode ser editado com sucesso");
                return HandleViewException(model, ex);
            }
        }

        public virtual ActionResult ListarCliente(int id)
        {
            var cliente = TClient.Load(id);
            return PartialView("_listar-cliente", cliente);
        }
        public virtual ActionResult ListarFilmes(int id)
        {
            var filme = TMovie.Load(id);
            return PartialView("_listar-filmes", filme);
        }

        public virtual ActionResult Excluir(int id)
        {
            var reserva = TReservation.Load(id);
            return PartialView("_excluir", reserva);
        }

        [HttpPost]
        public virtual ActionResult Excluir(int id, object diff)
        {
            TClient.Delete(x => x.Id == id);
            TIten.Delete(x => x.Id == id);
            TReservation.Delete(id);
            return RedirectToAction("Index");

        }
    }
}
//    {
//        public virtual ActionResult Index()
//        {
//            var reserva = TReservation.ListAll().ToList();
//            return View(reserva);
//        }

//        public virtual ActionResult Cadastrar()
//        {
//            var reserva = new TReservation();
//            ViewBag.Client = TClient.ListAll().ToSelectList(x => x.Id, x => x.Name);
//            ViewBag.Itens = TMovie.ListAll().ToSelectList(x => x.Id, x => x.Name);
//            return View(reserva);
//        }

//        [HttpPost]
//        public virtual ActionResult Cadastrar(TReservation model)  
//        {
//            try
//            {
//                TempData["Alert"] = new Alert("success", "Sua reserva foi cadastrada com sucesso");
//                model.Save();
//                TIten.SaveIten(model);
//                return RedirectToAction("Index");
//            }
//            catch (SimpleValidationException ex)
//            {
//                return HandleViewException(model, ex);
//            }
//        }

//       

//        [HttpPost]
//        public virtual ActionResult Editar(TReservation model)
//        {
//            try
//            {
//                model.Update();
//                TIten.SaveIten(model);
//                TempData["Alert"] = new Alert("success", "Sua reserva foi editada com sucesso");
//                return RedirectToAction("Index");
//            }
//            catch (SimpleValidationException ex)
//            {
//                return HandleViewException(model, ex);
//            }
//        }

//        public virtual ActionResult ListarCliente(int id)
//        {
//            var cliente = TClient.Load(id);
//            return PartialView("_listar-cliente", cliente);
//        }
//        public virtual ActionResult ListarFilmes(int id)
//        {
//            var filme = TMovie.Load(id);
//            return PartialView("_listar-filmes", filme);
//        }

//        public virtual ActionResult Excluir(int id)
//        {
//            var reserva = TReservation.Load(id);
//            return PartialView("_excluir", reserva);
//        }

//        [HttpPost]
//        public virtual ActionResult Excluir(int id, object diff)
//        {
//            TClient.Delete(x => x.Id == id);
//            TIten.Delete(x => x.Id == id);
//            TReservation.Delete(id);
//            return RedirectToAction("Index");

//        }
//    }
//}