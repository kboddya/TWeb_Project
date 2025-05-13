using System;
using System.Collections.Generic;

namespace BookShopProject.Models
{
    public class Order: UserMinimal
    {
        public Order(){}
        
        public Order(Domain.Entities.User.UserMinimal u): base(u)
        {
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public Book Book { get; set; }
    }

    public class OrderList: UserMinimal
    {
        public OrderList(){}
        
        public OrderList(Domain.Entities.User.UserMinimal u): base(u)
        {
        }
        public List<Order> Orders { get; set; } = new List<Order>();
    }

}
