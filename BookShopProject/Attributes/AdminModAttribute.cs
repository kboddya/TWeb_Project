using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.User;
using BookShopProject.Domain.Enums.User;
using BookShopProject.Extension;

namespace BookShopProject.Attributes
{
    public class AdminModAttribute : ActionFilterAttribute
    {
        private readonly ISession _sessionBL;

        public AdminModAttribute()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _sessionBL = bl.GetSessionBL();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpCookie = HttpContext.Current.Request.Cookies["WNCNN"];

            if (httpCookie == null)
            {
                filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary(new
                        { controller = "Errors", action = "er404" }));
            }

            var user = _sessionBL.GetUserByCookie(httpCookie.Value);
            if (user != null && (user.Role == URole.admin))
            {
                HttpContext.Current.SetMySessionObject(user);
            }
            else
            {
                filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary(new
                        { controller = "Errors", action = "er404" }));
            }
        }
    }
}