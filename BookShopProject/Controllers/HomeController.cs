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
            SessionStatus();
            if (System.Web.HttpContext.Current.Session["LoginStatus"] == "true")
            {
                ViewBag.loginStatus = "true";
                var user = System.Web.HttpContext.Current.GetMySessionObject();
                if (user != null)
                {
                    ViewBag.userName = user.Name;
                    ViewBag.userEmail = user.Email;
                    ViewBag.userRole = user.Role;
                }
                else
                {
                    ViewBag.loginStatus = "false";
                }
            }
            else
            {
                ViewBag.loginStatus = "false";
            }
            
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
                        ISBN = "978-3-16-148410-0"
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
                        ISBN = "978-1-23-456789-0"
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
                        ISBN = "978-0-12-345678-9"
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
                        ISBN = "978-9-87-654321-0"
                    }
                }
            };
            return View(data);
        }

        
    }
}