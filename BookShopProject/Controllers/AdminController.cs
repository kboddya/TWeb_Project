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

        public AdminController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _authorAdmin = bl.GetAuthorAdminBL();
            _bookAdmin = bl.GetBookAdminBL();
            _publisherAdmin = bl.GetPublisherAdmin();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Order()
        {
            var bl = new BusinessLogic.BusinessLogic();
            var orderBL = bl.GetOrderAdminBL();
            var ordersList = orderBL.GetOrders();

            var config = new AutoMapper.MapperConfiguration(cfg =>
                cfg.CreateMap<Domain.Entities.Order.OrderDbTable, Models.Order>());
            var mapper = config.CreateMapper();
            var orderListModel = new Models.OrderList();
            foreach (var v in ordersList.Orders)
            {
                orderListModel.Orders.Add(mapper.Map<Models.Order>(v));
            }

            return View(orderListModel);
        }

        public ActionResult OrderDetails(){
            var b = Request.QueryString["Id"];
            var bl = new BusinessLogic.BusinessLogic();
            var orderBL = bl.GetOrderAdminBL();
            var orderFromBL = orderBL.GetOrderById(int.Parse(b));
            if (orderFromBL == null) return RedirectToAction("er404", "Errors");
            var config = new AutoMapper.MapperConfiguration(cfg =>
                cfg.CreateMap<Domain.Entities.Order.OrderDbTable, Models.Order>());
            var mapper = config.CreateMapper();
            var order = mapper.Map<Models.Order>(orderFromBL);

            return View(order);
        }

        [HttpPost]
        public ActionResult OrderDetails(Order order)
        {
            var bl = new BusinessLogic.BusinessLogic();
            var orderBL = bl.GetOrderAdminBL();

            var orderDbTable = orderBL.GetOrderById(order.Id);

            orderDbTable.UserId = order.UserId;
            orderDbTable.Status = order.Status;
            orderDbTable.TotalPrice = order.TotalPrice;

            var result = orderBL.UpdateOrderStatus(order.Id, order.Status);

            return View(order);
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

            var bl = new BusinessLogic.BusinessLogic();

            var authorBL = bl.GetAuthorAdminBL();

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
            SessionStatus();
            var userMin = System.Web.HttpContext.Current.GetMySessionObject();
            if (userMin != null && userMin.Role != URole.admin) return RedirectToAction("Index", "Home");
            else if (userMin == null) return RedirectToAction("Login", "Auth");

            var config =
                new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<Book, Domain.Entities.Book.BookDbTable>());
            var mapper = config.CreateMapper();

            var bookDbTable = mapper.Map<Domain.Entities.Book.BookDbTable>(b);
            var result = _bookAdmin.CreateBook(bookDbTable);
            return RedirectToAction("BookList");
        }
        
        public ActionResult PublishersList()
        {
            SessionStatus();
            var userMin = System.Web.HttpContext.Current.GetMySessionObject();
            if (userMin != null && userMin.Role != URole.admin) return RedirectToAction("Index", "Home");
            else if (userMin == null) return RedirectToAction("Login", "Auth");
            
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
            
            SessionStatus();
            var userMin = System.Web.HttpContext.Current.GetMySessionObject();
            if (userMin != null && userMin.Role != URole.admin) return RedirectToAction("Index", "Home");
            else if (userMin == null) return RedirectToAction("Login", "Auth");
            
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
            SessionStatus();
            var userMin = System.Web.HttpContext.Current.GetMySessionObject();
            if (userMin != null && userMin.Role != URole.admin) return RedirectToAction("Index", "Home");
            else if (userMin == null) return RedirectToAction("Login", "Auth");

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
            SessionStatus();
            var userMin = System.Web.HttpContext.Current.GetMySessionObject();
            if (userMin != null && userMin.Role != URole.admin) return RedirectToAction("Index", "Home");
            else if (userMin == null) return RedirectToAction("Login", "Auth");

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
            SessionStatus();
            var userMin = System.Web.HttpContext.Current.GetMySessionObject();
            if (userMin != null && userMin.Role != URole.admin) return RedirectToAction("Index", "Home");
            else if (userMin == null) return RedirectToAction("Login", "Auth");
            
            var publisher = new Publisher();

            return View(publisher);
        }
 
        [HttpPost]
        public ActionResult AddPublisher(Publisher publisher)
        {
            SessionStatus();
            var userMin = System.Web.HttpContext.Current.GetMySessionObject();
            if (userMin != null && userMin.Role != URole.admin) return RedirectToAction("Index", "Home");
            else if (userMin == null) return RedirectToAction("Login", "Auth");

            var config =
                new AutoMapper.MapperConfiguration(
                    cfg => cfg.CreateMap<Models.Publisher, Domain.Entities.Publisher.PublisherDbTable>());
            var mapper = config.CreateMapper();

            var publisherDbTable = mapper.Map<Domain.Entities.Publisher.PublisherDbTable>(publisher);
            _publisherAdmin.CreatePublisher(publisherDbTable);
            return RedirectToAction("PublishersList");
        }
    }
}
