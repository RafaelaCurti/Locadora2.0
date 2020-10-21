using Locadora.Domain;
using Locadora.Web.Helpers;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Remotion.Data.Linq.Parsing.Structure.IntermediateModel;
using Simple.Validation;
using Simple.Web.Mvc;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;

namespace Locadora.Web.Areas.Cliente.Controllers

{
    [RequiresAuthorization]
    public partial class ReservasController : BaseController
    {

        public virtual ActionResult Index()
        {
            var usuarioLogado = (TClient)ViewBag.Cliente;
            var reserva = TReservation.ListAll().Where(x => x.Client.Login == usuarioLogado.Login).ToList();
            return View(reserva);
        }

        public virtual ActionResult Cadastrar()
        {
            var usuarioLogado = (TClient)ViewBag.Cliente;
            var reserva = new TReservation();
            ViewBag.Client = TClient.ListAll().Where(x => x.Login == usuarioLogado.Login).ToSelectList(x => x.Id, x => x.Name);
            ViewBag.Itens = TMovie.ListAll().ToSelectList(x => x.Id, x => x.Name);
            return View(reserva);
        }

        [HttpPost]
        public virtual ActionResult Cadastrar(TReservation model)
        {
            try
            {
                model.Client = (TClient)ViewBag.Cliente;
                if (model.Itens == null)
                    {
                        TempData["Alerta"] = new Alert("error", "Preencha o campo de filmes");
                        return RedirectToAction("Cadastrar");
                    }
                else
                    {
                        model.Save();
                        TIten.SaveIten(model);
                        TempData["Alerta"] = new Alert("success", "Reserva cadastrada com sucesso");
                        return RedirectToAction("Index");
                    }
            }
            catch (SimpleValidationException ex)
            {
                TempData["Alerta"] = new Alert("error", "Sua reserva não pode ser cadastrada");
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
    }
}