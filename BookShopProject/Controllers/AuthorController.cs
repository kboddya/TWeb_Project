using System;
using System.Web.Mvc;
using BookShopProject.Models;
using System.Collections.Generic;
using AutoMapper;
using BookShopProject.Domain.Entities.Author;
using BookShopProject.BusinessLogic;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Extension;


namespace BookShopProject.Controllers
{
    public class AuthorController : BaseController
    {
        private readonly IAuthorUser _authorUser;
        
        public AuthorController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _authorUser = bl.GetAuthorUserBL();
        }
        // GET
        public ActionResult Index()
        {
            SessionStatus();
            var AuthorListModel = new AuthorList(System.Web.HttpContext.Current.GetMySessionObject());

            var authorsList = _authorUser.GetAuthors();

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
            SessionStatus();
            var u = System.Web.HttpContext.Current.GetMySessionObject();
            
            var authorFromBL = _authorUser.GetAuthorById(int.Parse(b));
            if (authorFromBL == null)
            {
                return RedirectToAction("er404", "Errors");
            }
            
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AuthorDbTable, Author>());
            var mapper = config.CreateMapper();
            var author = mapper.Map<Author>(authorFromBL);
            
            if(u!= null)
            {
                author.IsAuthenticated = true;
                author.Role = u.Role;
                author.Email = u.Email;
                author.Name = u.Name;
            }
            
            var books = _authorUser.GetBooksByAuthorId(int.Parse(b));
            
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