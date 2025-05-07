using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Domain.Enums.Book;
using BookShopProject.Models;
using BookShopProject.Controllers;
using BookShopProject.Extension;

namespace BookShopProject.Controllers
{
    public class GenreController : BaseController
    {
        private readonly IBookUser _bookUser;

        public GenreController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _bookUser = bl.GetBookUserBL();
        }
        // GET
        public ActionResult Index()
        {
            var genres = _bookUser.GetGenres();

            SessionStatus();
            var genresList = new GenreList(System.Web.HttpContext.Current.GetMySessionObject())
            {
                Genres = new List<string>(genres.Count)
            };

            genresList.Genres.AddRange(genres.Select(g => g.Name));

            return View(genresList);
        }

        public ActionResult List()
        {
            var genre = Request.QueryString["Genre"];
            
            var config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<BookDbTable, Book>());
            var mapper = config.CreateMapper();
            
            SessionStatus();
            var List = new BookList(System.Web.HttpContext.Current.GetMySessionObject())
            {
                NameOfList = genre,
                Products = new List<Book>()
            };
            
            var b = _bookUser.GetBooks(genre, BSearchParameter.Genre);

            foreach (var v in b.Books)
            {
                List.Products.Add(mapper.Map<Book>(v));
            }

            return List.Products.Count != 0 ? RedirectToAction("BookList", "Book", List) : RedirectToAction("er404", "Errors");
        }
    }
}