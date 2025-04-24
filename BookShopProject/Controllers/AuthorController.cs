using System;
using System.Web.Mvc;
using BookShopProject.Models;
using System.Collections.Generic;
using AutoMapper;
using BookShopProject.Domain.Entities.Author;
using BookShopProject.BusinessLogic;
using BookShopProject.Domain.Entities.Book;


namespace BookShopProject.Controllers
{
    public class AuthorController : Controller
    {
        // GET
        public ActionResult Index()
        {
            var AuthorListModel = new AuthorList();

            var bl = new BusinessLogic.BusinessLogic();
            
            var authorBL = bl.GetAuthorUserBL();

            var authorsList = authorBL.GetAuthors();

            var config = new MapperConfiguration(cfg => cfg.CreateMap < AuthorDbTable, Author>());
            var mapper = config.CreateMapper();
            foreach (var v in authorsList.Authors)
            {
                AuthorListModel.Authors.Add(mapper.Map<Author>(v));
            }

            return View(AuthorListModel);
        }

        public ActionResult Details()
        {
            var b = Request.QueryString["Id"];

            var bl = new BusinessLogic.BusinessLogic();
            var authorBL = bl.GetAuthorUserBL();
            
            var authorFromBL = authorBL.GetAuthorById(int.Parse(b));
            if (authorFromBL == null)
            {
                return RedirectToAction("er404", "Errors");
            }
            
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AuthorDbTable, Author>());
            var mapper = config.CreateMapper();
            var author = mapper.Map<Author>(authorFromBL);
            
            var books = authorBL.GetBooksByAuthorId(int.Parse(b));
            
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<BookDbTable, Book>());
            var mapper2 = config2.CreateMapper();

            author.Books = new List<Book>();

            foreach (var book in books.Books)
            {
                author.Books.Add(mapper2.Map<Book>(book));
            }
            
            author.Genres = author.Genres ?? new List<string>();

            return View(author);
        }
    }
}