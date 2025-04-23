using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopProject.Domain.Entities.User;
using BookShopProject.Domain.Enums.User;
using BookShopProject.Extension;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Index()
        {
            var user = new Models.User();
            user.SetSession(SessionStatus());;
            if (!user.IsAuthenticated) return RedirectToAction("Login", "Auth");

            //TODO: Here must be cart logic
            return View(user);
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