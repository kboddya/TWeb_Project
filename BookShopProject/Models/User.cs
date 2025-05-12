using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookShopProject.Domain.Enums.User;

namespace BookShopProject.Models
{
	public class User: UserMinimal
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginTime { get; set; }
        public DateTime RegisterTime { get; set; }
    }
}