using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class AuthController : Controller
    {

        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var userData = new Domain.Entities.User.UDbTable
                {
                    LastIp = "localhost",
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password
                };
                var bl = new BusinessLogic.BusinessLogic();
                var sessionBL = bl.GetSessionBL();
                var result = sessionBL.UserRegister(userData);
                
                if (result.Status)
                {
                    // Registration successful, redirect to success page
                    return RedirectToAction("Success");
                }
                else
                {
                    // Registration failed, show error message
                    ModelState.AddModelError("", result.StatusMsg);
                }
            }
            return View(user);
        }
    }
}
