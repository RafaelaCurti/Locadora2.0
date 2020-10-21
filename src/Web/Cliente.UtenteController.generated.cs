// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Locadora.Web.Areas.Cliente.Controllers
{
    public partial class UtenteController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public UtenteController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected UtenteController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ListarPreferencia()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ListarPreferencia);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public UtenteController Actions { get { return MVC.Cliente.Utente; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Cliente";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Utente";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Utente";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Cadastrar = "Cadastrar";
            public readonly string ListarPreferencia = "ListarPreferencia";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Cadastrar = "Cadastrar";
            public const string ListarPreferencia = "ListarPreferencia";
        }


        static readonly ActionParamsClass_Cadastrar s_params_Cadastrar = new ActionParamsClass_Cadastrar();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Cadastrar CadastrarParams { get { return s_params_Cadastrar; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Cadastrar
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ListarPreferencia s_params_ListarPreferencia = new ActionParamsClass_ListarPreferencia();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ListarPreferencia ListarPreferenciaParams { get { return s_params_ListarPreferencia; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ListarPreferencia
        {
            public readonly string id = "id";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string _form = "_form";
                public readonly string _listar_preferencias = "_listar-preferencias";
                public readonly string cadastrar = "cadastrar";
            }
            public readonly string _form = "~/Areas/Cliente/Views/Utente/_form.cshtml";
            public readonly string _listar_preferencias = "~/Areas/Cliente/Views/Utente/_listar-preferencias.cshtml";
            public readonly string cadastrar = "~/Areas/Cliente/Views/Utente/cadastrar.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_UtenteController : Locadora.Web.Areas.Cliente.Controllers.UtenteController
    {
        public T4MVC_UtenteController() : base(Dummy.Instance) { }

        [NonAction]
        partial void CadastrarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Cadastrar()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Cadastrar);
            CadastrarOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void CadastrarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Locadora.Domain.TClient model);

        [NonAction]
        public override System.Web.Mvc.ActionResult Cadastrar(Locadora.Domain.TClient model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Cadastrar);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            CadastrarOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void ListarPreferenciaOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult ListarPreferencia(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ListarPreferencia);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ListarPreferenciaOverride(callInfo, id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
