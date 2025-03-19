using System.Web;
using System.Web.Mvc;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class BookController : Controller
    {
        public ActionResult BookInfo()
        {
            var b = Request.QueryString["b"];

            var book = new Book()
            {
                Title = "Portrait photography",
                Author = "Adam Silber",
                Genre = "Photography",
                Price = (decimal)40.00,
                Description = "A comprehensive guide to portrait photography.",
                Image = "https://m.media-amazon.com/images/I/714tSjSc4RL.jpg",
                Count = 10,
                Language = "English",
                Year = 2021,
                Publisher = "PhotoBooks Publishing",
                ISBN = b
            };
            return View(book);
        }

        [HttpPost]
        public ActionResult BookInfo(string btn)
        {
            return RedirectToAction("BookInfo", "Book", btn);
        }
    }
}