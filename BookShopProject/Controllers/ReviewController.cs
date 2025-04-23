using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class ReviewController : BaseController
    {
        public ActionResult Index()
        {
            var user = new UserMinimal();
            user.SetSession(SessionStatus());
            return View(user);
        }
    }
}