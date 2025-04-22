using System.Collections.Generic;
using System.Web.Mvc;
using BookShopProject.Domain.Enums.Book;

namespace BookShopProject.Controllers
{
    public class SearchController : Controller
    {
        
        [HttpPost]
        public ActionResult Search()
        {
            var search = Request.QueryString["search"];
            var bl = new BusinessLogic.BusinessLogic();
            var bookBL = bl.GetBookUserBL();
            var config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<Domain.Entities.Book.BookDbTable, Models.Book>());
            var mapper = config.CreateMapper();
            var b = bookBL.GetBooks(search, BSearchParameter.Title);
            var data = new Models.BookList();
            data.Products = new List<Models.Book>();
            foreach (var v in b.Books)
            {
                data.Products.Add(mapper.Map<Models.Book>(v));
            }
            
            return View(data);
        }
    }
}