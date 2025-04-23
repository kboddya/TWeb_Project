using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class AboutController : BaseController
    {
        public ActionResult Index()
        {
            var user = new UserMinimal();
            user.SetSession(SessionStatus());;
            
            return View(user);
        }

        [HttpPost]
        public ActionResult SendMessage(string yourName, string yourEmail, string subject, string message)
        {
            TempData["SuccessMessage"] = "Your message has been sent successfully!";

            return RedirectToAction("Index");
        }
    }
}
