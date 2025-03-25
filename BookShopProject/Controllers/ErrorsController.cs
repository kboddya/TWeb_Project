using System.Web.Mvc;

namespace BookShopProject.Controllers
{
    public class ErrorsController : Controller
    {
        
        public ActionResult er404()
        {
            return View();
        }
    }
}