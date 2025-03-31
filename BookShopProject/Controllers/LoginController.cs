using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopProject.Models;
using BookShopProject.Domain.Entities.User;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.BusinessLogic;

namespace BookShopProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISession _session;
        public LoginController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User login)
        {
            // if (ModelState.IsValid)
            // {
            //     UDbTable data = new UDbTable
            //     {
            //         Credential = login.Credential,
            //         Password = login.Password,
            //         LoginIp = Request.UserHostAddress,
            //         LoginDateTime = DateTime.Now
            //     };
            //     var userLogin = _session.UserLogin(data);
            //     if (userLogin.Status)
            //     {
            //         return RedirectToAction("Index", "Home");
            //     }
            //     else
            //     {
            //         ModelState.AddModelError("", userLogin.StatusMsg);
            //         return View("~/Views/Auth/Login.cshtml");
            //     }
            // }
            return View("~/Views/Auth/Login.cshtml");
        }
    }
}
