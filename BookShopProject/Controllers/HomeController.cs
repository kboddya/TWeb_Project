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
        
        public HomeController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _bookUser = bl.GetBookUserBL();
        }
        // GET: Home
        public ActionResult Index()
        {
            SessionStatus();
            var data = new BookList(System.Web.HttpContext.Current.GetMySessionObject());
            
            var config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<BookDbTable, Book>());
            var mapper = config.CreateMapper();
            var b = _bookUser.GetBooks(" ",BSearchParameter.Popularity);

            data.Products = new List<Book>();
            foreach (var v in b.Books)
            {
                data.Products.Add(mapper.Map<Book>(v));
            }
            
            return View(data);
        }

        public ActionResult AccessDenied()
        {
            return View();
        }


    }
}