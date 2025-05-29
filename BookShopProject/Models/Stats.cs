using System.Collections.Generic;

namespace BookShopProject.Models
{
    public class Stats 
    {
        public List<Book> Books { get; set; }
        
        public List<Author> Authors { get; set; }
        
        public List<Genre> Genre { get; set; }
        
        public List<Publisher> Publishers { get; set; }
    }
}