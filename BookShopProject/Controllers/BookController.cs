using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Domain.Enums.Book;
using BookShopProject.Extension;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class BookController : BaseController
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

            SessionStatus();
            var u = System.Web.HttpContext.Current.GetMySessionObject();
            var book = mapper.Map<Book>(_bookUser.GetBookByISBN(long.Parse(Request.QueryString["ISBN"])));

            if (book != null)
            {
                book.IsAuthenticated = true;
                book.Role = u.Role;
                book.Email = u.Email;
                book.Name = u.Name;
            }
            

            return book != null ? (ActionResult)View(book) : RedirectToAction("er404", "Errors");
        }

        public ActionResult BookList()
        {
            var genre = Request.QueryString["Genre"];
            var Search = Request.QueryString["Search"];
            var Year = Request.QueryString["Year"];
            var Lang = Request.QueryString["Lang"];
            var Age = Request.QueryString["Age"];
            var Publisher = Request.QueryString["Publisher"];

            var config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<BookDbTable, Book>());
            var mapper = config.CreateMapper();
            
            SessionStatus();
            var List = new BookList(System.Web.HttpContext.Current.GetMySessionObject())
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
            else if (Search != null)
            {
                List.NameOfList = Search;
                List.parameterForSearch = Search;
                var b = _bookUser.GetBooks(Search, BSearchParameter.Title);
                foreach (var v in b.Books)
                {
                    List.Products.Add(mapper.Map<Book>(v));
                }
            }
            else if (Year != null)
            {
                List.NameOfList = Year;

                var b = _bookUser.GetBooks(Year, BSearchParameter.Year);

                foreach (var v in b.Books)
                {
                    List.Products.Add(mapper.Map<Book>(v));
                }
            }
            else if (Lang != null)
            {
                List.NameOfList = Lang;

                var b = _bookUser.GetBooks(Lang, BSearchParameter.Language);

                foreach (var v in b.Books)
                {
                    List.Products.Add(mapper.Map<Book>(v));
                }
            }
            else if (Age != null)
            {
                List.NameOfList = Age;

                var b = _bookUser.GetBooks(Age, BSearchParameter.Age);

                foreach (var v in b.Books)
                {
                    List.Products.Add(mapper.Map<Book>(v));
                }
            }
            else if (Publisher != null)
            {
                List.NameOfList = Publisher;

                var b = _bookUser.GetBooks(Publisher, BSearchParameter.Publisher);

                foreach (var v in b.Books)
                {
                    List.Products.Add(mapper.Map<Book>(v));
                }
            }

            return List.Products.Count != 0 ? (ActionResult)View(List) : RedirectToAction("er404", "Errors");
        }
    }
}