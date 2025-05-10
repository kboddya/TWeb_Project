using BookShopProject.Domain.Entities.Articles;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IArticleAdmin: IArticleUser
    {
        bool UpdateArticle(ArticleDbTable article);
        
        bool AddArticle(ArticleDbTable article);
        
        bool DeleteArticle(int id);
    }
}