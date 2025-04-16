using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShopProject.Models
{
    public class Book
    {
        public ulong ISBN { get; set; } = 9780000000000;
        
        public string Title { get; set; }

        public string AuthorFirstName { get; set; }
        
        public string AuthorLastName { get; set; }

        public string Genre { get; set; }

        public decimal Price { get; set; }

        public decimal DiscountedPrice { get; set; } = decimal.MinusOne;

        public string Description { get; set; }

        public string Image { get; set; }

        public int Count { get; set; }

        public string Language { get; set; }

        public DateTime Year { get; set; }

        public string Publisher { get; set; }
        

        public DateTime PublishDate { get; set; }
        
        public int CountOfOrders { get; set; } = -1;
    }
}