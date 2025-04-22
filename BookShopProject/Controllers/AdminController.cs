using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BookShopProject.Domain.Enums.Book;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly BusinessLogic.BusinessLogic bl = new BusinessLogic.BusinessLogic();

        // GET
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AuthorList()
        {
            var authorBL = bl.GetAuthorAdminBL();
            var authorsList = authorBL.GetAuthors();

            var config = new AutoMapper.MapperConfiguration(cfg =>
                cfg.CreateMap<Domain.Entities.Author.AuthorDbTable, Models.Author>());
            var mapper = config.CreateMapper();
            var authorListModel = new Models.AuthorList();
            foreach (var v in authorsList.Authors)
            {
                authorListModel.Authors.Add(mapper.Map<Models.Author>(v));
            }

            return View(authorListModel);
        }

        public ActionResult AuthorDetails()
        {
            var b = Request.QueryString["Id"];

            var authorBL = bl.GetAuthorAdminBL();

            var authorFromBL = authorBL.GetAuthorById(int.Parse(b));
            if (authorFromBL == null)
            {
                return RedirectToAction("er404", "Errors");
            }

            var config = new AutoMapper.MapperConfiguration(cfg =>
                cfg.CreateMap<Domain.Entities.Author.AuthorDbTable, Models.Author>());
            var mapper = config.CreateMapper();
            var author = mapper.Map<Models.Author>(authorFromBL);

            return View(author);
        }

        [HttpPost]
        public ActionResult AuthorDetails(Author author)
        {
            var authorBL = bl.GetAuthorAdminBL();
            var config = new AutoMapper.MapperConfiguration(cfg =>
                cfg.CreateMap<Models.Author, Domain.Entities.Author.AuthorDbTable>());
            var mapper = config.CreateMapper();
            var authorDbTable = mapper.Map<Domain.Entities.Author.AuthorDbTable>(author);

            var result = authorBL.UpdateAuthor(authorDbTable);

            if (result)
            {
                return RedirectToAction("AuthorList");
            }

            ModelState.AddModelError("Error", "Error updating author");

            return View(author);
        }

        public ActionResult BookList(string search = "none", BSearchParameter type = BSearchParameter.All)
        {
            var bookBL = bl.GetBookAdminBL();
            var booksList = bookBL.GetBooks(search, type);

            var config = new AutoMapper.MapperConfiguration(cfg =>
                cfg.CreateMap<Domain.Entities.Book.BookDbTable, Book>());
            var mapper = config.CreateMapper();
            var bookListModel = new BookList
            {
                Products = new List<Book>()
            };

            foreach (var v in booksList.Books)
            {
                bookListModel.Products.Add(mapper.Map<Book>(v));
            }

            return View(bookListModel);
        }

        [HttpPost]
        public ActionResult BookList(BookList b)
        {
            return BookList(b.parameterForSearch, b.typeOfSearch);
        }

        public ActionResult BookDetails()
        {
            var b = Request.QueryString["ISBN"];
            var bookBL = bl.GetBookAdminBL();
            var bookFromBL = bookBL.GetBooks(b, BSearchParameter.ISBN);
            if (bookFromBL == null)
            {
                return RedirectToAction("er404", "Errors");
            }

            var config = new AutoMapper.MapperConfiguration(cfg =>
                cfg.CreateMap<Domain.Entities.Book.BookDbTable, Models.Book>());
            var mapper = config.CreateMapper();
            var book = mapper.Map<Models.Book>(bookFromBL.Books[0]);

            return View(book);
        }

        [HttpPost]
        public ActionResult BookDetails(Book book)
        {
            var bookBl = bl.GetBookAdminBL();


            var config =
                new AutoMapper.MapperConfiguration(
                    cfg => cfg.CreateMap<Models.Book, Domain.Entities.Book.BookDbTable>());
            var mapper = config.CreateMapper();
            var bookDbTable = mapper.Map<Domain.Entities.Book.BookDbTable>(book);
            var result = bookBl.UpdateBook(bookDbTable);
            if (result)
            {
                return RedirectToAction("BookList");
            }

            return View(book);
        }

        public ActionResult DeleteBook()
        {
            var b = Request.QueryString["ISBN"];
            var bookBL = bl.GetBookAdminBL();
            var bookFromBL = bookBL.GetBooks(b, BSearchParameter.ISBN);
            if (bookFromBL == null)
            {
                return RedirectToAction("er404", "Errors");
            }

            return bookBL.DeleteBook(bookFromBL.Books[0].Id)
                ? RedirectToAction("BookList")
                : RedirectToAction("BookDetails", new { ISBN = bookFromBL.Books[0].ISBN });
        }

        public ActionResult AddBook()
        {
            var b = new Book()
            {
                PublishDate = DateTime.Today
            };
            return View(b);
        }

        [HttpPost]
        public ActionResult AddBook(Book b)
        {
            var bookBl = bl.GetBookAdminBL();

            var config =
                new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<Book, Domain.Entities.Book.BookDbTable>());
            var mapper = config.CreateMapper();

            var bookDbTable = mapper.Map<Domain.Entities.Book.BookDbTable>(b);
            var result = bookBl.CreateBook(bookDbTable);
            return RedirectToAction("BookList");
        }
    }
}