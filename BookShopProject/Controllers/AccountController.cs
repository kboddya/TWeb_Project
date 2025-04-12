using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopProject.Domain.Entities.User;
using BookShopProject.Domain.Enums.User;
using BookShopProject.Extension;

namespace BookShopProject.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Index()
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "false" || user == null) return RedirectToAction("Login", "Auth");
            
            ViewBag.userName = user.Name;
            ViewBag.userEmail = user.Email;

            //TODO: Here must be cart logic
            return View();
        }

        [HttpPost]
        public ActionResult Index(string idOfAction)
        {
            switch (idOfAction)
            {
                case "1":
                {
                    DestroySession();
                    return RedirectToAction("Index", "Home");
                    break;
                }
                
                default:
                    return RedirectToAction("Index", "Home");
                    break;
            }
            
        }
    }
}