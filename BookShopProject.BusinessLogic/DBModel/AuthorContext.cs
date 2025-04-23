using System.Data.Entity;
using BookShopProject.Domain.Entities.Author;

namespace BookShopProject.BusinessLogic
{
    public class AuthorContext: DbContext
    {
        public AuthorContext(): base("name=BookShopProject")
        {
        }
        public virtual DbSet<AuthorDbTable> Authors { get; set; }
    }
}