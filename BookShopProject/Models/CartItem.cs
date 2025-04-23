using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShopProject.Models
{
    public class CartItem: UserMinimal
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
