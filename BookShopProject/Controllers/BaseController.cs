using System.Linq;
using System.Web.Mvc;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Extension;

namespace BookShopProject.Controllers
{
    public class BaseController : Controller
    {
        private readonly ISession _session;

        public BaseController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }

        public void DestroySession()
        {
            System.Web.HttpContext.Current.Session.Clear();

            if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("WNCNN"))
            {
                var cookie = ControllerContext.HttpContext.Request.Cookies["WNCNN"];
                if (cookie != null)
                {
                    cookie.Expires = System.DateTime.Now.AddDays(-1);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                }
            }
        }

        public void SessionStatus()
        {
            var httpCookie = Request.Cookies["WNCNN"];
            if (httpCookie != null)
            {
                var user = _session.GetUserByCookie(httpCookie.Value);

                if (user != null)
                {
                    System.Web.HttpContext.Current.Session["LoginStatus"] = "true";
                    System.Web.HttpContext.Current.SetMySessionObject(user);
                    return;
                }
                else
                {
                    System.Web.HttpContext.Current.Session.Clear();

                    if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("WNCNN")) DestroySession();
                    
                }
            }

            System.Web.HttpContext.Current.Session["LoginStatus"] = "false";
        }
    }
}