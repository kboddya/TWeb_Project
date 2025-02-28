using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShopProject.Controllers
{
    public class AboutController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(string yourName, string yourEmail, string subject, string message)
        {
            TempData["SuccessMessage"] = "Your message has been sent successfully!";

            return RedirectToAction("Index");
        }
    }
}
