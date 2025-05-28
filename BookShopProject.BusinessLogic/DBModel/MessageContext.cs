using System.Data.Entity;

namespace BookShopProject.BusinessLogic.DBModel
{
    public class MessageContext : DbContext
    {
        public MessageContext() : base("BookShopProject")
        {
        }

        public virtual DbSet<Domain.Entities.User.MessageDbTable> Messages { get; set; }
    }
}