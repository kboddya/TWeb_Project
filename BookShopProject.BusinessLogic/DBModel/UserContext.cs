using System.Data.Entity;
using BookShopProject.Domain.Entities.User;

namespace BookShopProject.BusinessLogic
{
    public class UserContext : DbContext
    {
        public UserContext() : base("name=BookShopProject")
        {
        }

        public virtual DbSet<UDbTable> Users { get; set; }
    }
}