using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.User;
using BookShopProject.Extension;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Domain.Enums.Book;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBookUser _bookUser;
        private readonly IAuthorUser _authorUser;
        private readonly IPublisherUser _publisherUser;

        public HomeController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _bookUser = bl.GetBookUserBL();
            _authorUser = bl.GetAuthorUserBL();
            _publisherUser = bl.GetPublisherUserBL();
        }

        // GET: Home
        public ActionResult Index()
        {
            SessionStatus();
            var data = new HomePage(_bookUser.GetBooks("", BSearchParameter.Popularity).Books,
                _bookUser.GetGenresByPopularity(), _authorUser.GetAuthorsByPopularity(),
                _publisherUser.GetPublishersByPopularity(), System.Web.HttpContext.Current.GetMySessionObject());

            return View(data);
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}