using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookShopProject.Domain.Enums.User;

namespace BookShopProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public URole Role { get; set; }
        public DateTime LastLoginTime { get; set; }
        public DateTime RegisterTime { get; set; }
    }
}