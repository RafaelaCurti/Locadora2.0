using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple.Web.Mvc;
using System.CodeDom.Compiler;
using Locadora.Domain;

namespace Locadora.Web.Controllers
{
    public partial class HomeController : Controller
    {

        public virtual ActionResult Index()
        {
            var filmes = TMovie.ListAll().ToList();
            return View(filmes);
        }
        public virtual ActionResult ListarGenero(int id)
        {
            var genero = TCategory.Load(id);
            return PartialView("_listar-generos", genero);
        }
    }
}
