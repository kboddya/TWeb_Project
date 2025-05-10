using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BookShopProject.BusinessLogic;
using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.User;
using BookShopProject.Domain.Enums.User;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserPanel _userPanel;

        public AdminController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _userPanel = bl.GetUserPanelBL();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserControlPanel()
        {
            var users = _userPanel.GetAll();

            var userl = new UserListModel { Details = new List<User>()};


            foreach(var user in users)
            {
                userl.Details.Add(new Models.User { 
                    Id =  user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                });
            }

            return View(userl);
        }

        public ActionResult UserDetails()
        {
            var id = Request.QueryString["id"];
            var user = _userPanel.GetById(int.Parse(id));
            if (user == null)
            {
                return HttpNotFound();
            }

            var u = new User {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Password = user.Password,
                LastLoginTime = user.LastLoginTime,
                RegisterTime = user.RegisterTime,
            };
            return View(u);
        }

        [HttpPost]
        public ActionResult UserDetails(User model)
        {
            var user = new UDbTable
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Role = model.Role,
                Password = model.Password,
                LastLoginTime = model.LastLoginTime,
                RegisterTime = model.RegisterTime
            };
            _userPanel.Update(user);
            return View(model);
        }

        public ActionResult DeleteUser()
        {
            var id = Request.QueryString["id"];
            _userPanel.Delete(int.Parse(id));
            return RedirectToAction("UserControlPanel");
        }
    }
}