using System.Collections.Generic;

namespace BookShopProject.Models
{
    public class BookList
    {
        public string NameOfList { get; set; }
        public List<Book> Products { get; set; }
    }
}