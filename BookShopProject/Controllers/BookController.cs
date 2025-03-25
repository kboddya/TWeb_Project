using System.Web;
using System.Web.Mvc;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class BookController : Controller
    {
        public ActionResult BookInfo()
        {
            var b = Request.QueryString["ISBN"];

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
                ISBN = "978-1-23-456789-0"
            };

            return b == book.ISBN ? (ActionResult)View(book) : RedirectToAction("er404", "Errors");
        }
    }
}