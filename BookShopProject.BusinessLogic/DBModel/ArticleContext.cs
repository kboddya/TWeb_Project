using System.Data.Entity;
using BookShopProject.Domain.Entities.Articles;

namespace BookShopProject.BusinessLogic.DBModel
{
    public class ArticleContext: DbContext
    {
        public ArticleContext(): base("BookShopProject")
        {
        }
        
        public virtual DbSet<ArticleDbTable> Articles { get; set; }
    }
}