using System.Collections.Generic;
using BookShopProject.Domain.Entities.Articles;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IArticleUser
    {
        List<ArticleDbTable> GetArticles();
        
        ArticleDbTable GetArticleById(int id);
    }
}