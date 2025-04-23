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
            var bl = new BusinessLogic.BusinessLogic();
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

            var bl = new BusinessLogic.BusinessLogic();

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
            var bl = new BusinessLogic.BusinessLogic();
            var authorBL = bl.GetAuthorAdminBL();
            
            var authorDbTable = authorBL.GetAuthorById(author.Id);
            
            authorDbTable.Image = author.Image;
            authorDbTable.BirthDate = author.BirthDate;
            authorDbTable.DeathDate = author.DeathDate;
            authorDbTable.Wiki = author.Wiki;

            var result = authorBL.UpdateAuthor(authorDbTable);

            if (result)
            {
                return RedirectToAction("AuthorList");
            }

            ModelState.AddModelError("Error", "Error updating author");
            return View(author);
        }
    }
}
