using System;
using System.Collections.Generic;
using BookShopProject.Domain.Enums.Order;

namespace BookShopProject.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public List<Book> Books { get; set; }
    }

    public class OrderList
    {
        public List<Order> Orders { get; set; } = new List<Order>();
    }

}
