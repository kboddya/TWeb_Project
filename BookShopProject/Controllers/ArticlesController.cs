using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Models;

namespace BookShopProject.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleUser _articleUser;

        public ArticlesController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _articleUser = bl.GetArticleUserBL();
        }

        public ActionResult ArticlesList()
        {
            var listFromDb = _articleUser.GetArticles();

            var config = new AutoMapper.MapperConfiguration(cfg =>
                cfg.CreateMap<Domain.Entities.Articles.ArticleDbTable, Article>());
            var mapper = config.CreateMapper();

            var list = new ArticleList()
            {
                Articles = new List<Article>()
            };

            foreach (var v in listFromDb)
            {
                list.Articles.Add(mapper.Map<Article>(v));
            }

            return View(list);
        }

        public ActionResult ArticleDetails()
        {
            var id = Request.QueryString["Id"];

            if (id == null) return RedirectToAction("er404", "Errors");
            
            var articleFromBL = _articleUser.GetArticleById(int.Parse(id));
            
            if (articleFromBL != null)
            {
                var config = new AutoMapper.MapperConfiguration(cfg =>
                    cfg.CreateMap<Domain.Entities.Articles.ArticleDbTable, Article>());
                var mapper = config.CreateMapper();

                var articleModel = mapper.Map<Article>(articleFromBL);
                return View(articleModel);
            }


            return RedirectToAction("er404", "Errors");
        }
    }
}