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

        public ActionResult OrderList()
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

        public ActionResult OrderDetails()
        {
            var b = Request.QueryString["Id"];

            var bl = new BusinessLogic.BusinessLogic();
            var orderBL = bl.GetOrderAdminBL();

            var orderFromBL = orderBL.GetOrderById(int.Parse(b));
            if (orderFromBL == null)
            {
                return RedirectToAction("er404", "Errors");
            }

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
            
            if (result)
            {
                return RedirectToAction("AuthorList");
            }

            ModelState.AddModelError("Error", "Error updating author");

            return View(order);
        }
    }
}
