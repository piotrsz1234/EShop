using System.Web.Mvc;
using System.Web.Routing;

namespace EShop.Web.Helpers
{
    public class EnforceUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (SessionHelper.LoggedUser == null)
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new {controller = "Account", action = "SignIn"}));
            base.OnActionExecuting(filterContext);
        }
    }
}