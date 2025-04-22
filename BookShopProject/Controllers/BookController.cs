using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Domain.Enums.Book;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookUser _bookUser;
        public BookController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _bookUser = bl.GetBookUserBL();
        }
        public ActionResult BookInfo()
        {
            var config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<BookDbTable, Book>());
            var mapper = config.CreateMapper();
            
            var book = mapper.Map<Book>(_bookUser.GetBookByISBN(long.Parse(Request.QueryString["ISBN"])));

            return book != null ? (ActionResult)View(book) : RedirectToAction("er404", "Errors");
        }

        public ActionResult BookList()
        {
            var genre = Request.QueryString["Genre"];
            var Search = Request.QueryString["Search"];
            var Year = Request.QueryString["Year"];
            var Lang = Request.QueryString["Lang"];

            var config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<BookDbTable, Book>());
            var mapper = config.CreateMapper();
            var List = new BookList()
            {
                Products = new List<Book>()
            };
            
            if (genre != null)
            {
                List.NameOfList = genre;
                
                var b = _bookUser.GetBooks(genre, BSearchParameter.Genre);

                foreach (var v in b.Books)
                {
                    List.Products.Add(mapper.Map<Book>(v));
                }
            }
            else if(Search != null)
            {
                List.NameOfList = Search;
                List.parameterForSearch = Search;
                var b = _bookUser.GetBooks(Search, BSearchParameter.Title);
                foreach (var v in b.Books)
                {
                    List.Products.Add(mapper.Map<Book>(v));
                }
            }
            
            return List.Products.Count != 0 ? (ActionResult)View(List) : RedirectToAction("er404", "Errors");
        }
    }
}