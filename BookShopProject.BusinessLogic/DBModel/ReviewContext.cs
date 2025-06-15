using System.Data.Entity;

namespace BookShopProject.BusinessLogic.DBModel
{
    public class ReviewContext: DbContext
    {
        public ReviewContext() : base("BookShopProject")
        {
        }
        
        public virtual DbSet<Domain.Entities.Book.ReviewDbTable> Reviews { get; set; }
    }
}