using Locadora.Domain;
using Locadora.Web.Areas.ViewModels;
using Locadora.Web.Helpers;
using Simple.Entities;
using Simple.Validation;
using Simple.Web.Mvc;
using System.Web.Mvc;

namespace Locadora.Web.Areas.Painel.Controllers
{
    [RequiresAuthorization]
    public partial class ReservasController : BaseController
    {      
        public virtual ActionResult Index()
         {
            ViewBag.Pagina = 1;
            var reserva = (TReservation)ViewBag.Reserva;
            var buscar = new ReservationSearchViewModel().ConvertToReservationSearch();
            var reservas = TReservation.Search(buscar);
            ViewBag.Itens = TMovie.ListAll().ToSelectList(x => x.Id, x => x.Name);
            var total = TReservation.CountSearch(buscar);
            var page = new Page<TReservation>(reservas, total);
            return View(page);
        }  
        public virtual ActionResult Buscar(ReservationSearchViewModel model)
        {
            model.pagina = model.pagina ?? 1;
            ViewBag.Pagina = model.pagina;
            var buscar = model.ConvertToReservationSearch();
            var reserva = TReservation.Search(buscar);
            var total = TReservation.CountSearch(buscar);
            var page = new Page<TReservation>(reserva, total);
            return PartialView("_buscar", page);
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
                if (model.Itens != null && model.Itens.Length > 0)
                {
                    model.Save();
                    TIten.SaveIten(model);
                    TempData["Alerta"] = new Alert("success", "Reserva cadastrada com sucesso");
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Alerta"] = new Alert("error", "Preencha o campo de filme  ");
                    return RedirectToAction("Cadastrar");
                }

            }
            catch (SimpleValidationException ex)
            {
                ViewBag.TempData["Alerta"] = new Alert("error", "Reserva não pode ser cadastrada com sucesso");
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
                if (model.Itens != null && model.Itens.Length > 0)
                {
                    model.Update();
                    TIten.SaveIten(model);
                    TempData["Alerta"] = new Alert("success", "Reserva editada com sucesso");
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Alerta"] = new Alert("error", "Preencha o campo de filme");
                    return RedirectToAction("Editar");
                }
            }
            catch (SimpleValidationException ex)
            {
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