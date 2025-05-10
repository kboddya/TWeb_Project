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
        
        public CartController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _orderUser = bl.GetOrderUserBL();
        }

        public ActionResult Cart()
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();

            if (user != null)
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
            foreach (var v in ordersList.Orders)
            {
                orderListModel.Orders.Add(mapper.Map<Order>(v));
            }

            return View(orderListModel);
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