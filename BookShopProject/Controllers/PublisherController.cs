using System.Collections.Generic;
using System.Web.Mvc;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Domain.Entities.Publisher;
using BookShopProject.Domain.Enums.User;
using BookShopProject.Extension;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class PublisherController : BaseController
    {
        private readonly IPublisherPublisher _publisherPublisher;

        private readonly IPublisherUser _publisherUser;

        public PublisherController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _publisherPublisher = bl.GetPublisherPublisherBL();
            _publisherUser = bl.GetPublisherUserBL();
        }

        public ActionResult Index()
        {
            var id = Request.QueryString["Id"];
            SessionStatus();
            var u = System.Web.HttpContext.Current.GetMySessionObject();

            var publisher = _publisherUser.GetPublisherById(int.Parse(id));

            var mapper = new AutoMapper.MapperConfiguration(cfg => { cfg.CreateMap<PublisherDbTable, Publisher>(); });
            var mapperConfig = mapper.CreateMapper();
            var publisherModel = mapperConfig.Map<Publisher>(publisher);

            if (publisherModel != null && u != null)
            {
                publisherModel.IsAuthenticated = true;
                publisherModel.Role = u.Role;
                publisherModel.Email = u.Email;
                publisherModel.Name = u.Name;
            }

            if (publisherModel == null)
            {
                return RedirectToAction("er404", "Errors");
            }

            var books = _publisherUser.GetBooksByPublisherId(int.Parse(id));

            var bookMapper = new AutoMapper.MapperConfiguration(cfg => { cfg.CreateMap<BookDbTable, Book>(); });
            var bookMapperConfig = bookMapper.CreateMapper();


            publisherModel.Books = new List<Book>();
            foreach (var b in books.Books)
            {
                publisherModel.Books.Add(bookMapperConfig.Map<Book>(b));
            }

            return View(publisherModel);
        }

        public ActionResult Panel()
        {
            SessionStatus();
            var u = System.Web.HttpContext.Current.GetMySessionObject();

            if (u == null) return RedirectToAction("Login", "Auth");

            if (u.Role != URole.publisher)
            {
                return RedirectToAction("er404", "Errors");
            }

            var publisher = _publisherPublisher.PublisherByEmail(u.Email);

            var mapper = new AutoMapper.MapperConfiguration(cfg => { cfg.CreateMap<PublisherDbTable, Publisher>(); });
            var mapperConfig = mapper.CreateMapper();
            var publisherModel = mapperConfig.Map<Publisher>(publisher);

            if (publisherModel != null)
            {
                publisherModel.IsAuthenticated = true;
                publisherModel.Role = u.Role;
                publisherModel.Email = u.Email;
                publisherModel.Name = u.Name;
            }

            if (publisherModel == null)
            {
                return RedirectToAction("er404", "Errors");
            }

            var books = _publisherUser.GetBooksByPublisherId(publisherModel.Id);

            var bookMapper = new AutoMapper.MapperConfiguration(cfg => { cfg.CreateMap<BookDbTable, Book>(); });
            var bookMapperConfig = bookMapper.CreateMapper();
            publisherModel.Books = new List<Book>();
            foreach (var b in books.Books)
            {
                publisherModel.Books.Add(bookMapperConfig.Map<Book>(b));
            }

            return View(publisherModel);
        }

        public ActionResult BookDetails()
        {
            SessionStatus();
            var u = System.Web.HttpContext.Current.GetMySessionObject();

            if (u == null) return RedirectToAction("Login", "Auth");
            if (u.Role != URole.publisher)
            {
                return RedirectToAction("er404", "Errors");
            }

            var publisher = _publisherPublisher.PublisherByEmail(u.Email);
            if (u.Email != publisher?.Email)
            {
                return RedirectToAction("er404", "Errors");
            }

            var ISBN = Request.QueryString["ISBN"];

            var book = _publisherPublisher.BookByIsbn(ISBN);

            var mapper = new AutoMapper.MapperConfiguration(cfg => { cfg.CreateMap<BookDbTable, Book>(); });
            var mapperConfig = mapper.CreateMapper();
            var bookModel = mapperConfig.Map<Book>(book);
            if (bookModel != null)
            {
                bookModel.IsAuthenticated = true;
                bookModel.Role = u.Role;
                bookModel.Email = u.Email;
                bookModel.Name = u.Name;
            }
            else
            {
                return RedirectToAction("er404", "Errors");
            }

            return View(bookModel);
        }

        [HttpPost]
        public ActionResult BookDetails(Book book)
        {
            SessionStatus();
            var u = System.Web.HttpContext.Current.GetMySessionObject();
            if (u == null) return RedirectToAction("Login", "Auth");
            if (u.Role != URole.publisher)
            {
                return RedirectToAction("er404", "Errors");
            }

            var publisher = _publisherPublisher.PublisherByEmail(u.Email);
            if (u.Email != publisher?.Email)
            {
                return RedirectToAction("er404", "Errors");
            }

            if (book.Publisher != publisher.Name || book.PublisherId != publisher.Id) return RedirectToAction("er404", "Errors");

            var mapper = new AutoMapper.MapperConfiguration(cfg => { cfg.CreateMap<Book, BookDbTable>(); });
            var mapperConfig = mapper.CreateMapper();

            return _publisherPublisher.UpdateBook(mapperConfig.Map<BookDbTable>(book))
                ? (ActionResult)RedirectToAction("Panel")
                : View(book);
        }
        
        public ActionResult AddBook()
        {
            SessionStatus();
            var u = System.Web.HttpContext.Current.GetMySessionObject();
            if (u == null) return RedirectToAction("Login", "Auth");
            if (u.Role != URole.publisher)
            {
                return RedirectToAction("er404", "Errors");
            }

            var publisher = _publisherPublisher.PublisherByEmail(u.Email);
            if (u.Email != publisher?.Email)
            {
                return RedirectToAction("er404", "Errors");
            }
            
            

            return View(new Book()
            {
                IsAuthenticated = true,
                Role = u.Role,
                Email = u.Email,
                Name = u.Name,
                PublisherId = publisher.Id,
                Publisher = publisher.Name
            });
        }

        [HttpPost]
        public ActionResult AddBook(Book book)
        {
            SessionStatus();
            var u = System.Web.HttpContext.Current.GetMySessionObject();
            if (u == null) return RedirectToAction("Login", "Auth");
            if (u.Role != URole.publisher)
            {
                return RedirectToAction("er404", "Errors");
            }

            var publisher = _publisherPublisher.PublisherByEmail(u.Email);
            if (u.Email != publisher?.Email)
            {
                return RedirectToAction("er404", "Errors");
            }
            
            if (book.Publisher != publisher.Name || book.PublisherId != publisher.Id) return RedirectToAction("er404", "Errors");

            var mapper = new AutoMapper.MapperConfiguration(cfg => { cfg.CreateMap<Book, BookDbTable>(); });
            var mapperConfig = mapper.CreateMapper();

            return _publisherPublisher.CreateBook(mapperConfig.Map<BookDbTable>(book))
                ? (ActionResult)RedirectToAction("Panel")
                : View(book);
        }
        
        public ActionResult DeleteBook()
        {
            SessionStatus();
            var u = System.Web.HttpContext.Current.GetMySessionObject();
            if (u == null) return RedirectToAction("Login", "Auth");
            if (u.Role != URole.publisher)
            {
                return RedirectToAction("er404", "Errors");
            }

            var publisher = _publisherPublisher.PublisherByEmail(u.Email);
            if (u.Email != publisher?.Email)
            {
                return RedirectToAction("er404", "Errors");
            }
            

            var ISBN = Request.QueryString["ISBN"];

            var book = _publisherPublisher.BookByIsbn(ISBN);
            if (book?.Publisher != publisher.Name || book?.PublisherId != publisher.Id) return RedirectToAction("er404", "Errors");
            _publisherPublisher.DeleteBook(book.Id);
            
            return RedirectToAction("Panel");
        }
    }
}