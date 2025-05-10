using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BookShopProject.Attributes;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Enums.Book;
using BookShopProject.Domain.Enums.User;
using BookShopProject.Extension;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    [AdminMod]
    public class AdminController : BaseController
    {
        private readonly IAuthorAdmin _authorAdmin;
        private readonly IBookAdmin _bookAdmin;
        private readonly IPublisherAdmin _publisherAdmin;
        private readonly IArticleAdmin _articleAdmin;

        public AdminController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _authorAdmin = bl.GetAuthorAdminBL();
            _bookAdmin = bl.GetBookAdminBL();
            _publisherAdmin = bl.GetPublisherAdmin();
            _articleAdmin = bl.GetArticleAdminBL();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AuthorList()
        {
            var authorsList = _authorAdmin.GetAuthors();
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

            var authorFromBL = _authorAdmin.GetAuthorById(int.Parse(b));
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
            var config = new AutoMapper.MapperConfiguration(cfg =>
                cfg.CreateMap<Models.Author, Domain.Entities.Author.AuthorDbTable>());
            var mapper = config.CreateMapper();
            var authorDbTable = mapper.Map<Domain.Entities.Author.AuthorDbTable>(author);

            var result = _authorAdmin.UpdateAuthor(authorDbTable);

            if (result)
            {
                return RedirectToAction("AuthorList");
            }

            ModelState.AddModelError("Error", "Error updating author");

            return View(author);
        }

        public ActionResult DeleteAuthor()
        {
            var id = Request.QueryString["Id"];

            return _authorAdmin.DeleteAuthor(int.Parse(id))
                ? RedirectToAction("AuthorList")
                : RedirectToAction("er404", "Errors");
        }

        public ActionResult BookList(string search = "none", BSearchParameter type = BSearchParameter.All)
        {
            var booksList = _bookAdmin.GetBooks(search, type);

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
            var bookFromBL = _bookAdmin.GetBooks(b, BSearchParameter.ISBN);
            if (bookFromBL == null)
            {
                return RedirectToAction("er404", "Errors");
            }

            var config = new AutoMapper.MapperConfiguration(cfg =>
                cfg.CreateMap<Domain.Entities.Book.BookDbTable, Models.Book>());
            var mapper = config.CreateMapper();

            var book = new Book();
            book = mapper.Map<Models.Book>(bookFromBL.Books[0]);

            return View(book);
        }

        [HttpPost]
        public ActionResult BookDetails(Book book)
        {
            var config =
                new AutoMapper.MapperConfiguration(
                    cfg => cfg.CreateMap<Models.Book, Domain.Entities.Book.BookDbTable>());
            var mapper = config.CreateMapper();
            var bookDbTable = mapper.Map<Domain.Entities.Book.BookDbTable>(book);
            var result = _bookAdmin.UpdateBook(bookDbTable);
            if (result)
            {
                return RedirectToAction("BookList");
            }

            return View(book);
        }

        public ActionResult DeleteBook()
        {
            var b = Request.QueryString["ISBN"];
            var bookFromBL = _bookAdmin.GetBooks(b, BSearchParameter.ISBN);
            if (bookFromBL == null)
            {
                return RedirectToAction("er404", "Errors");
            }

            return _bookAdmin.DeleteBook(bookFromBL.Books[0].Id)
                ? RedirectToAction("BookList")
                : RedirectToAction("BookDetails", new { ISBN = bookFromBL.Books[0].ISBN });
        }

        public ActionResult AddBook()
        {
            SessionStatus();
            var userMin = System.Web.HttpContext.Current.GetMySessionObject();
            if (userMin != null && userMin.Role != URole.admin) return RedirectToAction("Index", "Home");
            else if (userMin == null) return RedirectToAction("Login", "Auth");
            var b = new Book()
            {
                PublishDate = DateTime.Today
            };

            return View(b);
        }

        [HttpPost]
        public ActionResult AddBook(Book b)
        {
            var config =
                new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<Book, Domain.Entities.Book.BookDbTable>());
            var mapper = config.CreateMapper();

            var bookDbTable = mapper.Map<Domain.Entities.Book.BookDbTable>(b);
            var result = _bookAdmin.CreateBook(bookDbTable);
            return RedirectToAction("BookList");
        }

        public ActionResult PublishersList()
        {
            var publishersList = _publisherAdmin.GetPublishers();

            var config = new AutoMapper.MapperConfiguration(cfg =>
                cfg.CreateMap<Domain.Entities.Publisher.PublisherDbTable, Models.Publisher>());
            var mapper = config.CreateMapper();

            var publisherListModel = new Models.PublishersList();
            foreach (var v in publishersList.Publishers)
            {
                publisherListModel.Publishers.Add(mapper.Map<Models.Publisher>(v));
            }

            return View(publisherListModel);
        }

        public ActionResult PublisherDetails()
        {
            var b = Request.QueryString["Id"];

            var publisherFromBL = _publisherAdmin.GetPublisherById(int.Parse(b));

            if (publisherFromBL == null)
            {
                return RedirectToAction("er404", "Errors");
            }

            var config = new AutoMapper.MapperConfiguration(cfg =>
                cfg.CreateMap<Domain.Entities.Publisher.PublisherDbTable, Models.Publisher>());
            var mapper = config.CreateMapper();
            var publisher = mapper.Map<Models.Publisher>(publisherFromBL);

            return View(publisher);
        }

        [HttpPost]
        public ActionResult PublisherDetails(Publisher publisher)
        {
            var config =
                new AutoMapper.MapperConfiguration(
                    cfg => cfg.CreateMap<Models.Publisher, Domain.Entities.Publisher.PublisherDbTable>());
            var mapper = config.CreateMapper();
            var publisherDbTable = mapper.Map<Domain.Entities.Publisher.PublisherDbTable>(publisher);

            var result = _publisherAdmin.UpdatePublisher(publisherDbTable);

            if (result)
            {
                return RedirectToAction("PublishersList");
            }

            ModelState.AddModelError("Error", "Error updating publisher");

            return View(publisher);
        }

        public ActionResult DeletePublisher()
        {
            var b = Request.QueryString["Id"];
            var publisherFromBL = _publisherAdmin.GetPublisherById(int.Parse(b));
            if (publisherFromBL == null)
            {
                return RedirectToAction("er404", "Errors");
            }

            return _publisherAdmin.DeletePublisher(publisherFromBL.Id)
                ? RedirectToAction("PublishersList")
                : RedirectToAction("PublisherDetails", new { Id = publisherFromBL.Id });
        }

        public ActionResult AddPublisher()
        {
            var publisher = new Publisher();

            return View(publisher);
        }

        [HttpPost]
        public ActionResult AddPublisher(Publisher publisher)
        {
            var config =
                new AutoMapper.MapperConfiguration(
                    cfg => cfg.CreateMap<Models.Publisher, Domain.Entities.Publisher.PublisherDbTable>());
            var mapper = config.CreateMapper();

            var publisherDbTable = mapper.Map<Domain.Entities.Publisher.PublisherDbTable>(publisher);
            _publisherAdmin.CreatePublisher(publisherDbTable);
            return RedirectToAction("PublishersList");
        }

        public ActionResult ArticlesList()
        {
            var listFromDb = _articleAdmin.GetArticles();

            var config = new AutoMapper.MapperConfiguration(cfg =>
                cfg.CreateMap<Domain.Entities.Articles.ArticleDbTable, Article>());
            var mapper = config.CreateMapper();

            var list = new ArticleList()
            {
                Articles = new List<Article>()
            };
            foreach (var v in listFromDb)
            {
                list.Articles.Add(mapper.Map<Article>(v));
            }

            return View(list);
        }

        public ActionResult ArticleDetails()
        {
            var id = Request.QueryString["Id"];

            if (id == null) return RedirectToAction("er404", "Errors");

            var articleFromBL = _articleAdmin.GetArticleById(int.Parse(id));

            if (articleFromBL != null)
            {
                var config = new AutoMapper.MapperConfiguration(cfg =>
                    cfg.CreateMap<Domain.Entities.Articles.ArticleDbTable, Article>());
                var mapper = config.CreateMapper();

                var articleModel = mapper.Map<Article>(articleFromBL);
                return View(articleModel);
            }


            return RedirectToAction("er404", "Errors");
        }

        [HttpPost]
        public ActionResult ArticleDetails(Article article)
        {
            var config =
                new AutoMapper.MapperConfiguration(
                    cfg => cfg.CreateMap<Models.Article, Domain.Entities.Articles.ArticleDbTable>());
            var mapper = config.CreateMapper();
            var articleDbTable = mapper.Map<Domain.Entities.Articles.ArticleDbTable>(article);

            return _articleAdmin.UpdateArticle(articleDbTable)
                ? RedirectToAction("ArticlesList")
                : (ActionResult)View(article);
        }

        public ActionResult DeleteArticle()
        {
            var id = Request.QueryString["Id"];

            return _articleAdmin.DeleteArticle(int.Parse(id))
                ? RedirectToAction("ArticlesList")
                : RedirectToAction("er404", "Errors");
        }

        public ActionResult AddArticle()
        {
            var article = new Article();

            return View(article);
        }

        [HttpPost]
        public ActionResult AddArticle(Article article)
        {
            var config =
                new AutoMapper.MapperConfiguration(
                    cfg => cfg.CreateMap<Models.Article, Domain.Entities.Articles.ArticleDbTable>());
            var mapper = config.CreateMapper();

            var articleDbTable = mapper.Map<Domain.Entities.Articles.ArticleDbTable>(article);

            return _articleAdmin.AddArticle(articleDbTable)
                ? RedirectToAction("ArticlesList")
                : (ActionResult)View(article);
        }
    }
}