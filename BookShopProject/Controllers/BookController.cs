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

            var reviews = _bookUser.GetReviews(book.ISBN);
            
            var configReviews = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<ReviewDbTable, Review>());
            var mapperReviews = configReviews.CreateMapper();

            if (book != null)
            {
                book.IsAuthenticated = true;
                book.Role = u.Role;
                book.Email = u.Email;
                book.Name = u.Name;
                book.Reviews = new ReviewList();
            }
            foreach (var v in reviews)
            {
                book.Reviews.Reviews.Add(mapperReviews.Map<Review>(v));
            }
            
            return book != null ? (ActionResult)View(book) : RedirectToAction("er404", "Errors");
        }

        [HttpPost]
        public ActionResult BookInfo(Book book)
        {
            SessionStatus();
            var u = System.Web.HttpContext.Current.GetMySessionObject();
            if (u == null)
            {
                return RedirectToAction("er404", "Errors");
            }
            var config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<Review, ReviewDbTable>());
            var mapper = config.CreateMapper();
            var r = mapper.Map<ReviewDbTable>(book.Review);
            r.ISBN = book.ISBN;
            r.Date = DateTime.Now;
            r.Email = u.Email;
            _bookUser.AddReview(r);
            return RedirectToAction("BookInfo", new { ISBN = book.ISBN });
        }

        public ActionResult BookList(string type = null)
        {
            var config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<BookDbTable, Book>());
            var mapper = config.CreateMapper();

            SessionStatus();
            var List = new BookList(System.Web.HttpContext.Current.GetMySessionObject())
            {
                Products = new List<Book>()
            };
            
            if (type == "Offers")
            {
                List.NameOfList = "Offers";
                var b = _bookUser.GetBooks("Offers", BSearchParameter.Offers);
                foreach (var v in b.Books)
                {
                    List.Products.Add(mapper.Map<Book>(v));
                }
                return List.Products.Count != 0 ? (ActionResult)View(List) : RedirectToAction("er404", "Errors");
            }
            else if(type == "Popular")
            {
                List.NameOfList = "Popularity";
                var b = _bookUser.GetBooks("", BSearchParameter.Popularity);
                foreach (var v in b.Books)
                {
                    List.Products.Add(mapper.Map<Book>(v));
                }
                return List.Products.Count != 0 ? (ActionResult)View(List) : RedirectToAction("er404", "Errors");
            }
            
            var genre = Request.QueryString["Genre"];
            var Search = Request.QueryString["Search"];
            var Year = Request.QueryString["Year"];
            var Lang = Request.QueryString["Lang"];
            var Age = Request.QueryString["Age"];
            var Publisher = Request.QueryString["Publisher"];



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