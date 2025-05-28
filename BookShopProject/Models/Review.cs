using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShopProject.Models
{
    public class Review
    {
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        
        public string Email { get; set; }
        
        public int Id { get; set; }
    }
}