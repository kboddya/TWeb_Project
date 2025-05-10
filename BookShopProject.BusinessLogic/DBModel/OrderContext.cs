using System.Data.Entity;
using BookShopProject.Domain.Entities.Book;

namespace BookShopProject.BusinessLogic
{
    public class OrderContext : DbContext
    {
        public OrderContext() : base("name=BookShopProject")
        {
        }

        public virtual DbSet<OrderDbTable> Orders { get; set; }
    }
}
