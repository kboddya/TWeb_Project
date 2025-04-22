using System.Data.Entity;
using BookShopProject.Domain.Entities.Genre;

namespace BookShopProject.BusinessLogic
{
    public class GenreContext: DbContext
    {
        public GenreContext() : base("name=BookShopProject")
        {
        }

        public virtual DbSet<GenreDbTable> Genres { get; set; }
    }
}