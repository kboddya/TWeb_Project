using System.Collections.Generic;
using System.Web.Mvc;
using BookShopProject.Models;
using BookShopProject.BusinessLogic;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Book;
using BookShopProject.Extension;

namespace BookShopProject.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        private readonly IOrderUser _orderUser;
        private readonly IBookUser _bookUser;
        
        public AccountController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _orderUser = bl.GetOrderUserBL();
            _bookUser = bl.GetBookUserBL();
        }


        public ActionResult Index()
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            if (user == null) return RedirectToAction("Login", "Auth");

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

        public ActionResult SignOut()
        {
            SessionStatus();
            DestroySession();

            return RedirectToAction("Index", "Home");
        }
    }
}