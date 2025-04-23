using System.Data.Entity;
using BookShopProject.Domain.Entities.User;

namespace BookShopProject.BusinessLogic.DBModel
{
    public class SessionContext: DbContext
    {
        public SessionContext() : base("name=BookShopProject")
        {
        }
        
        public virtual DbSet<Session> Sessions { get; set; }
    }
}