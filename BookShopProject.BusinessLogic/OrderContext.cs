using System.Data.Entity;
using BookShopProject.Domain.Entities.Order;

namespace BookShopProject.BusinessLogic
{
    public class OrderContext : DbContext
    {
        public OrderContext() : base("name=BookShopProject")
        {
        }

        public virtual DbSet<OrderDbTable> Order { get; set; }
    }
}
