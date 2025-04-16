using System.Data.Entity;
using BookShopProject.Domain.Entities.Book;

namespace BookShopProject.BusinessLogic
{
    public class BookContext: DbContext
    {
        public BookContext(): base("name=BookShopProject")
        {
        }
        
        public DbSet<BookDbTable> Books { get; set; }
    }
}