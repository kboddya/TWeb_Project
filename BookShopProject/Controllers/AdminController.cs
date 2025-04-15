using System.Web.Mvc;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class AdminController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
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