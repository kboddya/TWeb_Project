using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Enums.User;
using BookShopProject.Extension;

namespace BookShopProject.Attributes
{
    public class PublisherModAttribute: ActionFilterAttribute
    {
        private readonly ISession _sessionBL;
        
        public PublisherModAttribute()
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
            if (user != null && user.Role == URole.publisher)
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