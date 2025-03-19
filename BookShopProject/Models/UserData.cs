using System.Collections.Generic;

namespace BookShopProject.Models
{
    public class userData
    {
        public string Username { get; set; }
        public List<Book> Products { get; set; }
        public Book SingleProduct { get; set; }
    }
}