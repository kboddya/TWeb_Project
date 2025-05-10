using System.Collections.Generic;
using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Articles;

namespace BookShopProject.BusinessLogic
{
    public class ArticleAdminBL: AdminApi, IArticleAdmin
    {
        public List<ArticleDbTable> GetArticles()
        {
            return ArticlesListAction();
        }

        public ArticleDbTable GetArticleById(int id)
        {
            return ArticleByIdAction(id);
        }

        public bool UpdateArticle(ArticleDbTable article)
        {
            return UpdateArticleAction(article);
        }

        public bool AddArticle(ArticleDbTable article)
        {
            return CreateArticleAction(article);
        }

        public bool DeleteArticle(int id)
        {
            return DeleteArticleAction(id);
        }
    }
}