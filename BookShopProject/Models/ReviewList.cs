using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShopProject.Models
{
    public class ReviewList: UserMinimal
    {
        public List<Review> Reviews { get; set; }
    }
}