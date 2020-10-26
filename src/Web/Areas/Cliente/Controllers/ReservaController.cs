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

                {
                    var usuarioLogado = (TClient)ViewBag.Cliente;
                    model.Client = new TClient(usuarioLogado.Id);
                    //for (int i = 0; i < model.Itens.Length; i++)

                    //if (model.Itens.Any())
                    if (model.Itens != null && model.Itens.Length > 0)
                    //if (model.Itens[0] != 0)
                    {
                        //var usuarioLogado = (TClient)ViewBag.Cliente;
                        //model.Id = usuarioLogado.Id;
                        //var cliente = TClient.Load(usuarioLogado.Id);
                        //cliente.Name = model.Name;
                        //cliente.Email = model.Email;
                        //cliente.Telephone = model.Telephone;
                        //cliente.EnumProfileClient = model.EnumProfileClient;
                        //cliente.Preference = model.Preference;
                        //cliente.Update();
                        //TPreference.SavePreferences(model);
                        //TempData["Alerta"] = new Alert("success", "Cliente editado com sucesso");
                        //return RedirectToAction(MVC.Cliente.Home.Index());

                        //model.Update();
                        model.Save();
                        TIten.SaveIten(model);
                        TempData["Alerta"] = new Alert("success", "Reserva cadastrada com sucesso");
                        return RedirectToAction("Index");
                    }
                    //else (model.Itens.Length < 0)
                    else
                    //(!model.Itens.Any())
                    {
                        TempData["Alerta"] = new Alert("error", "Preencha o campo de filme");
                        return RedirectToAction("Cadastrar");
                    }


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