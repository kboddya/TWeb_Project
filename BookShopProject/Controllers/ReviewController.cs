using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopProject.Extension;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class ReviewController : BaseController
    {
        public ActionResult Index()
        {
            SessionStatus();
            var user = new Models.UserMinimal(System.Web.HttpContext.Current.GetMySessionObject());
            
            return View(user);
        }
    }
}