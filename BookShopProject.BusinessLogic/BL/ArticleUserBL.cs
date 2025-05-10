using System.Collections.Generic;
using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Articles;

namespace BookShopProject.BusinessLogic
{
    public class ArticleUserBL : UserApi, IArticleUser
    {
        public List<ArticleDbTable> GetArticles()
        {
            return ArticlesListAction();
        }

        public ArticleDbTable GetArticleById(int id)
        {
            return ArticleByIdAction(id);
        }
    }
}