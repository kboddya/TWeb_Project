using System.Data.Entity;
using BookShopProject.Domain.Entities.Publisher;

namespace BookShopProject.BusinessLogic
{
    public class PublisherContext: DbContext
    {
        public PublisherContext() : base("name=BookShopProject")
        {
        }
        
        public virtual DbSet<PublisherDbTable> Publishers { get; set; }
    }
}