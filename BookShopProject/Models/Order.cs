using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShopProject.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public List<Book> Books { get; set; }
    }

}
