using System.Web.Mvc;

namespace BookShopProject.Controllers
{
    public class ArticlesController : Controller
    {
        // GET
        public ActionResult ArticlesList()
        {
            return View();
        }
        
        public ActionResult ArticleDetails()
        {
            return View();
        }
    }
}