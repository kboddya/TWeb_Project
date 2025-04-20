using System.Collections.Generic;
using System.Linq;
using System;
using System.Web.Mvc;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Order()
        {
            var bl = new BusinessLogic.BusinessLogic();
            var orderBL = bl.GetOrderAdminBL();
            var orderList = orderBL.GetAllOrders();

            var orderListModel = new List<Models.Order>();
            foreach (var v in orderList)
            {
                var order = new Models.Order
                {
                    Id = v.Id,
                    UserId = v.UserId,
                    CreateTime = v.CreateTime,
                    LastUpdateTime = v.LastUpdateTime,
                    TotalPrice = v.TotalPrice,
                    Books = v.ISBNs.Select(isbn => new Book
                    {
                        ISBN = isbn
                    }).ToList()
                };
                orderListModel.Add(order);
            }

            return View(orderListModel);
        }

        public ActionResult OrderDetails()
        {
            var orderId = Request.QueryString["Id"];

            var bl = new BusinessLogic.BusinessLogic();
            var orderBL = bl.GetOrderAdminBL();

            var orderFromBL = orderBL.OrderById(int.Parse(orderId));
            if (orderFromBL == null)
            {
                return RedirectToAction("er404", "Errors");
            }

            var order = new Models.Order
            {
                Id = orderFromBL.Id,
                UserId = orderFromBL.UserId,
                CreateTime = orderFromBL.CreateTime,
                LastUpdateTime = orderFromBL.LastUpdateTime,
                TotalPrice = orderFromBL.TotalPrice,
                Books = orderFromBL.ISBNs.Select(isbn => new Book
                {
                    ISBN = isbn
                }).ToList()
            };

            return View(order);
        }

        [HttpPost]
        public ActionResult OrderDetails(Order order)
        {
            var bl = new BusinessLogic.BusinessLogic();
            var orderBL = bl.GetOrderAdminBL();

            var orderDbTable = orderBL.OrderById(order.Id);

            orderDbTable.Status = order.Status;
            orderDbTable.LastUpdateTime = DateTime.Now;

            var result = orderBL.UpdateOrderStatus(order.Id, order.Status);

            if (result)
            {
                return RedirectToAction("Orders");
            }

            ModelState.AddModelError("Error", "Error updating order");

            return View(order);
        }
    }
}
