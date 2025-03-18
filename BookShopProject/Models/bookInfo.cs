using System.Collections.Generic;

namespace BookShopProject.Models
{
    public class bookInfo
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
    
    public class userData
    {
        public string Username { get; set; }
        public List<bookInfo> Products { get; set; }
        public string SingleProduct { get; set; }
    }
}