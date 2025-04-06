using System.Web.Mvc;

namespace BookShopProject.Controllers
{
    public class AdminController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}