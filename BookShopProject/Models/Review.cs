using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShopProject.Models
{
    public class Review
    {
        public string BookName { get; set; }

        public string Author { get; set; }

        public string Comment { get; set; }

        public string Image { get; set; }

        public int CountOfMark { get; set; }

        public ulong ISBN { get; set; } = 9780000000000;

        public DateTime PublishDate { get; set; }

    }
}