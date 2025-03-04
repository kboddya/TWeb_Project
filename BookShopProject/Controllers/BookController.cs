using System.Web.Mvc;

namespace BookShopProject.Controllers
{
    public class BookController : Controller
    {
        // GET
        public ActionResult BookInfo()
        {
            return View();
        }
    }
}