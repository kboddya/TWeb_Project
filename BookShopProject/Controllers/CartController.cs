using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BookShopProject.Models;
using BookShopProject.BusinessLogic;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Extension;

namespace BookShopProject.Controllers
{
    public class CartController : BaseController
    {
        private readonly IOrderUser _orderUser;
        private readonly IBookUser _bookUser;

        public CartController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _orderUser = bl.GetOrderUserBL();
            _bookUser = bl.GetBookUserBL();
        }

        public ActionResult Index()
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();

            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var ordersList = _orderUser.GetOrders(user.Id);

            var config = new AutoMapper.MapperConfiguration(cfg =>
                cfg.CreateMap<OrderDbTable, Order>());
            var mapper = config.CreateMapper();
            var orderListModel = new OrderList(user)
            {
                Orders = new List<Order>()
            };
            var bookConfig = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<BookDbTable, Book>());
            var bookMapper = bookConfig.CreateMapper();
            foreach (var v in ordersList.Orders)
            {
                var order = mapper.Map<Order>(v);
                var bDb = _bookUser.GetBookByISBN(v.ISBN);
                order.Book = bookMapper.Map<Book>(bDb);
                orderListModel.Orders.Add(order);
            }

            return View(orderListModel);
        }

        public ActionResult AddToCart()
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            if (user == null) return RedirectToAction("Login", "Auth");

            var b = Request.QueryString["ISBN"];

            var bookFromBL = _bookUser.GetBookByISBN(long.Parse(b));
            if (bookFromBL == null) return RedirectToAction("er404", "Errors");

            var order = new OrderDbTable()
            {
                UserId = user.Id,
                ISBN = bookFromBL.ISBN,
                LastUpdateTime = DateTime.Now,
                CreateTime = DateTime.Now,
                IsBought = false,
                Price = bookFromBL.Price
            };


            return _orderUser.AddCart(order)
                ? RedirectToAction("BookInfo", "Book", new { ISBN = b })
                : RedirectToAction("er404", "Errors");
        }

        public ActionResult OrderDetails()
        {
            var b = Request.QueryString["Id"];
            var orderFromBL = _orderUser.GetOrderById(int.Parse(b));
            if (orderFromBL == null) return RedirectToAction("er404", "Errors");
            var config = new AutoMapper.MapperConfiguration(cfg =>
                cfg.CreateMap<Domain.Entities.Book.OrderDbTable, Models.Order>());
            var mapper = config.CreateMapper();
            var order = mapper.Map<Models.Order>(orderFromBL);

            return View(order);
        }
    }
}