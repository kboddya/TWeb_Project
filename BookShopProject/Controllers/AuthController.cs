﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly ISession _sessionBL;
        public AuthController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _sessionBL = bl.GetSessionBL();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (!ModelState.IsValid) return View(user);

            var userData = new Domain.Entities.User.UDbTable
            {
                LastIp = Request.UserHostAddress,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };

            var result = _sessionBL.UserRegister(userData);

            if (result.Status) return RedirectToAction("WelcomeToFamily");


            ModelState.AddModelError(result.StatusKey, result.StatusMsg);

            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (!ModelState.IsValid) return View(user);

            var userData = new Domain.Entities.User.UDbTable
            {
                LastIp = Request.UserHostAddress,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };

            var result = _sessionBL.UserLogin(userData);

            if (result.Status)
            {
                var cookie = _sessionBL.GenCookie(userData.Email);
                Response.Cookies.Add(cookie);
                
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(result.StatusKey, result.StatusMsg);
            return View(user);
        }

        public ActionResult WelcomeToFamily()
        {
            return View();
        }
    }
}