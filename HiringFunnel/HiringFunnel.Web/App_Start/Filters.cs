using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HiringFunnel.Data;
using HiringFunnel.Data.Models;

namespace HiringFunnel.Web
{

    public class AjaxOnly : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.HttpContext.Request.IsAjaxRequest();
        }
    }

    public class LoggedIn : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["login"] == null)
            {
                filterContext.Controller.TempData["errorMessage"] = "Potrebno je da budete ulogovani za pristup ovoj stranici.";
                filterContext.Controller.TempData["returnUrl"] = HttpContext.Current.Request.Url;
                filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Auth", action = "Login" }));
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }

    public class AdminLevel : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if ((HttpContext.Current.Session["login"] == null || (HttpContext.Current.Session["login"] != null
                && (HttpContext.Current.Session["login"] as User).Role > UserLevel.Admin)))
            {
                filterContext.Controller.TempData["errorMessage"] = "Potreban je administratorski nalog za pristup ovoj stranici.";
                filterContext.Controller.TempData["returnUrl"] = HttpContext.Current.Request.Url;
                filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Auth", action = "Login" }));
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }

    public class HRLevel : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if ((HttpContext.Current.Session["login"] == null || (HttpContext.Current.Session["login"] != null
                && (HttpContext.Current.Session["login"] as User).Role > UserLevel.HR)))
            {
                filterContext.Controller.TempData["errorMessage"] = "Potreban je HR nalog za pristup ovoj stranici.";
                filterContext.Controller.TempData["returnUrl"] = HttpContext.Current.Request.Url;
                filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Auth", action = "Login" }));
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }

}