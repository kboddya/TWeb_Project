using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var data = new userData
            {
                Username = "customer",
                Products = new List<bookInfo>
                {
                    new bookInfo
                    {
                        Title = "Portrait photography",
                        Author = "Adam Silber",
                        Genre = "Photography",
                        Price = 40.00,
                        Description = "A comprehensive guide to portrait photography.",
                        Image = "Content/images/tab-item1.jpg"
                    },
                    new bookInfo
                    {
                        Title = "Once upon a time",
                        Author = "Klien Marry",
                        Genre = "Fiction",
                        Price = 35.00,
                        Description = "A captivating tale of adventure and mystery.",
                        Image = "Content/images/tab-item2.jpg"
                    },
                    new bookInfo
                    {
                        Title = "Tips of simple lifestyle",
                        Author = "Bratt Smith",
                        Genre = "Lifestyle",
                        Price = 40.00,
                        Description = "Practical tips for living a simple and fulfilling life.",
                        Image = "Content/images/tab-item3.jpg"
                    },
                    new bookInfo
                    {
                        Title = "Just felt from outside",
                        Author = "Nicole Wilson",
                        Genre = "Fiction",
                        Price = 40.00,
                        Description = "An intriguing story of self-discovery and adventure.",
                        Image = "Content/images/tab-item4.jpg"
                    }
                }
            };
            return View(data);
        }
    }
}