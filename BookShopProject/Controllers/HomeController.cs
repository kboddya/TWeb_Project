using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopProject.Domain.Entities.User;
using BookShopProject.Extension;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
                        var data = new BookList
            {
                Products = new List<Book>
                {
                    new Book()
                    {
                        Title = "Portrait photography",
                        Author = "Adam Silber",
                        Genre = "Photography",
                        Price = (decimal)40.00,
                        Description = "A comprehensive guide to portrait photography.",
                        Image = "Content/images/tab-item1.jpg",
                        Count = 10,
                        Language = "English",
                        Year = 2021,
                        Publisher = "PhotoBooks Publishing",
                        ISBN = 9783161484100
                    },
                    new Book()
                    {
                        Title = "Once upon a time",
                        Author = "Klien Marry",
                        Genre = "Fiction",
                        Price = (decimal)35.00,
                        Description = "A captivating tale of adventure and mystery.",
                        Image = "Content/images/tab-item2.jpg",
                        Count = 15,
                        Language = "English",
                        Year = 2019,
                        Publisher = "StoryBooks Publishing",
                        ISBN = 9781234567890
                    },
                    new Book()
                    {
                        Title = "Tips of simple lifestyle",
                        Author = "Bratt Smith",
                        Genre = "Lifestyle",
                        Price = (decimal)40.00,
                        Description = "Practical tips for living a simple and fulfilling life.",
                        Image = "Content/images/tab-item3.jpg",
                        Count = 20,
                        Language = "English",
                        Year = 2020,
                        Publisher = "LifeBooks Publishing",
                        ISBN = 9780123456789
                    },
                    new Book()
                    {
                        Title = "Just felt from outside",
                        Author = "Nicole Wilson",
                        Genre = "Fiction",
                        Price = (decimal)40.00,
                        Description = "An intriguing story of self-discovery and adventure.",
                        Image = "Content/images/tab-item4.jpg",
                        Count = 5,
                        Language = "English",
                        Year = 2022,
                        Publisher = "AdventureBooks Publishing",
                        ISBN = 9789876543210
                    }
                }
            };
            SessionStatus();
            data.SetSession(System.Web.HttpContext.Current);

            return View(data);
        }

        
    }
}