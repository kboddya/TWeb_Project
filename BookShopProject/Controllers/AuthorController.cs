using System;
using System.Web.Mvc;
using BookShopProject.Models;
using System.Collections.Generic;


namespace BookShopProject.Controllers
{
    public class AuthorController : Controller
    {
        // GET
        public ActionResult Index()
        {
            // This is a placeholder for the actual implementation.

            var a = new AuthorList()
            {
                Authors = new List<Author>()
                {
                    new Author()
                    {
                        Id = 1,
                        Name = "George R.R. Martin",
                        Image =
                            "https://upload.wikimedia.org/wikipedia/commons/thumb/5/50/Portrait_photoshoot_at_Worldcon_75%2C_Helsinki%2C_before_the_Hugo_Awards_%E2%80%93_George_R._R._Martin_%28cropped%29.jpg/250px-Portrait_photoshoot_at_Worldcon_75%2C_Helsinki%2C_before_the_Hugo_Awards_%E2%80%93_George_R._R._Martin_%28cropped%29.jpg",
                        BirthDate = Convert.ToDateTime("1948-09-20"),
                        Wiki = "https://en.wikipedia.org/wiki/George_R._R._Martin",
                        Genres = new List<string> { "Fantasy", "Horror", "Science Fiction" }
                    },
                    new Author()
                    {
                        Id = 2,
                        Name = "J.R.R. Tolkien",
                        Image =
                            "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5d/J._K._Rowling_2010.jpg/250px-J._K._Rowling_2010.jpg",
                        BirthDate = Convert.ToDateTime("1892-01-03"),
                        DeathDate = Convert.ToDateTime("1973-09-02"),
                        Wiki = "https://en.wikipedia.org/wiki/J._R._R._Tolkien",
                        Genres = new List<string> { "Fantasy", "Adventure" }
                    }
                }
            };

            return View(a);
        }

        public ActionResult Details()
        {
            var b = Request.QueryString["Id"];

            var A = new Author()
            {
                Id = 1,
                Name = "George R.R. Martin",
                Image =
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/5/50/Portrait_photoshoot_at_Worldcon_75%2C_Helsinki%2C_before_the_Hugo_Awards_%E2%80%93_George_R._R._Martin_%28cropped%29.jpg/250px-Portrait_photoshoot_at_Worldcon_75%2C_Helsinki%2C_before_the_Hugo_Awards_%E2%80%93_George_R._R._Martin_%28cropped%29.jpg",
                BirthDate = Convert.ToDateTime("1948-09-20"),
                Wiki = "https://en.wikipedia.org/wiki/George_R._R._Martin",
                Genres = new List<string> { "Fantasy", "Horror", "Science Fiction" },
                Books = new List<Book>()
                {
                    new Book()
                    {
                        ISBN = 9780553103540,
                        Title = "A Game of Thrones",
                        Description = "The first book in A Song of Ice and Fire series.",
                        Image = "https://example.com/game_of_thrones.jpg",
                        PublishDate= new DateTime(1996, 8, 6)
                    },

                    new Book()
                    {
                        ISBN = 9780553108033,
                        Title = "A Clash of Kings",
                        Description = "The second book in A Song of Ice and Fire series.",
                        Image = "https://example.com/clash_of_kings.jpg",
                        PublishDate = new DateTime(1998, 11, 16)
                    }
                }
            };

            return b == A.Id.ToString() ? (ActionResult)View(A) : RedirectToAction("er404", "Errors");
        }
    }
}