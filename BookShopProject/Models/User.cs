using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShopProject.Models
{
	public class User: UserMinimal
    {
        public string Password { get; set; }
    }
}