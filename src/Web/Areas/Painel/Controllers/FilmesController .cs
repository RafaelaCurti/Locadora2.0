using Locadora.Domain;
using Locadora.Helpers;
using Locadora.Web.Areas.ViewModels;
using Locadora.Web.Helpers;
using Simple.Entities;
using Simple.Validation;
using Simple.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Locadora.Web.Areas.Painel.Controllers
{
    [RequiresAuthorization]
    public partial class FilmesController : BaseController
    {
        public virtual ActionResult Index()
        {
            ViewBag.Pagina = 1;
            var buscar = new MovieSearchViewModel().ConvertToMovieSearch();
            var filme = TMovie.Search(buscar);
            ViewBag.Category = TCategory.ListAll().ToSelectList(x => x.Id, x => x.Name);
            var total = TMovie.CountSearch(buscar);
            var page = new Page<TMovie>(filme, total);
            var filmeslistados = TMovie.ListAll().ToList();
            return View(page);
        }
        public virtual ActionResult Buscar(MovieSearchViewModel model)
        {
            model.pagina = model.pagina ?? 1;
            ViewBag.Pagina = model.pagina;
            var buscar = model.ConvertToMovieSearch();
            var filme = TMovie.Search(buscar);
            var total = TMovie.CountSearch(buscar);
            ViewBag.Category = TCategory.ListAll().ToSelectList(x => x.Id, x => x.Name);
            var page = new Page<TMovie>(filme, total);
            return PartialView("_buscar", page);
        }

        public virtual ActionResult Cadastrar()
        {
            var filme = new TMovie();
            ViewBag.EnumFormatMovie = EnumHelper.ListAll<FormatMovie>().ToSelectList(x => x, x => x.Description());
            ViewBag.EnumTypeMovie = EnumHelper.ListAll<TypeMovie>().ToSelectList(x => x, x => x.Description());
            ViewBag.Category = TCategory.ListAll().ToSelectList(x => x.Id, x => x.Name);
            return View(filme);
        }

        [HttpPost]
        public virtual ActionResult Cadastrar(TMovie model)
        {
            try
            {
                model.Date = DateTime.Now.GetCurrent();
                model.Save();
                TMovieCategory.SaveCategories(model);
                TempData["Alerta"] = new Alert("success", "Filme cadastrado com sucesso");
                //TempData["Alert"] = new Alert("success", "Seu filme foi cadastrado com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                ViewBag.EnumFormatMovie = EnumHelper.ListAll<FormatMovie>().ToSelectList(x => x, x => x.Description());
                ViewBag.EnumTypeMovie = EnumHelper.ListAll<TypeMovie>().ToSelectList(x => x, x => x.Description());
                //TempData["Alert"] = new Alert("error", "Seu filme não foi cadastrado");
                TempData["Alerta"] = new Alert("error", "O filme não pode ser cadastrado");
                return HandleViewException(model, ex);
            }
        }
        //public virtual ActionResult Cadastrar()
        //{
        //    var filme = new TMovie();
        //    ViewBag.EnumFormatMovie = EnumHelper.ListAll<FormatMovie>().ToSelectList(x => x, x => x.Description());
        //    ViewBag.EnumTypeMovie = EnumHelper.ListAll<TypeMovie>().ToSelectList(x => x, x => x.Description());
        //    ViewBag.Category = TCategory.ListAll().ToSelectList(x => x.Id, x => x.Name);
        //    return View(filme);
        //}

        //[HttpPost]
        //public virtual ActionResult Cadastrar(TMovie model)
        //{
        //    try
        //    {
        //        model.Date = DateTime.Now.GetCurrent();
        //        model.Save();
        //        TempData["Alert"] = new Alert("success", "Seu filme foi cadastrado com sucesso");
        //        TMovieCategory.SaveCategories(model);
        //        return RedirectToAction("Index");
        //    }
        //    catch (SimpleValidationException ex)
        //    {
        //        ViewBag.EnumFormatMovie = EnumHelper.ListAll<FormatMovie>().ToSelectList(x => x, x => x.Description());
        //        ViewBag.EnumTypeMovie = EnumHelper.ListAll<TypeMovie>().ToSelectList(x => x, x => x.Description());
        //        return HandleViewException(model, ex);
        //    }
        //}
        public virtual ActionResult Editar(int id)
        {
            var filme = TMovie.Load(id);
            var categoriasFilme = filme.TMovieCategories.Select(x => x.Category.Id).ToList();
            ViewBag.EnumFormatMovie = EnumHelper.ListAll<FormatMovie>().ToSelectList(x => x, x => x.Description());
            ViewBag.EnumTypeMovie = EnumHelper.ListAll<TypeMovie>().ToSelectList(x => x, x => x.Description());
            ViewBag.Category = TCategory.ListAll().Where(x => !categoriasFilme.Contains(x.Id)).ToSelectList(x => x.Id, x => x.Name);
            return View(filme);
        }

        [HttpPost]
        public virtual ActionResult Editar(TMovie model)
        {
            model.Update();
            TMovieCategory.SaveCategories(model);
            TempData["Alerta"] = new Alert("success", "Filme editado com sucesso");
            //TempData["Alert"] = new Alert("sucess", "Seu filme foi editado com sucesso");
            return RedirectToAction("Index");
        }

        public virtual ActionResult ListarGenero(int id)
        {
            var genero = TCategory.Load(id);
            return PartialView("_listar-generos", genero);
        }

        public virtual ActionResult Excluir(int id)
        {
            var filme = TMovie.Load(id);
            return PartialView("_excluir", filme);
        }

        [HttpPost]
        public virtual ActionResult Excluir(int id, object diff)
        {
            TMovieCategory.Delete(x => x.Movie.Id == id);
            TMovie.Delete(id);
            return RedirectToAction("Index");
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