using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShopProject.Domain.Entities.User;
using BookShopProject.Extension;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Domain.Enums.Book;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            var data = new BookList();
            
            var bl = new BusinessLogic.BusinessLogic();
            var bookBL = bl.GetBookUserBL(); 
            
            var config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<BookDbTable, Book>());
            var mapper = config.CreateMapper();
            var b = bookBL.GetBooks(" ",BSearchParameter.Popularity);

            data.Products = new List<Book>();
            foreach (var v in b.Books)
            {
                data.Products.Add(mapper.Map<Book>(v));
            }
            
            return View(data);
        }

        
    }
}