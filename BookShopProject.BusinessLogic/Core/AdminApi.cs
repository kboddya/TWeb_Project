using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Order;

namespace BookShopProject.BusinessLogic.Core
{
    public class AdminApi
    {
        internal OrderDbTable OrderByIdAction(int id)
        {
            OrderDbTable a;
            using(var db = new OrderContext()) { 
                a = db.Order.FirstOrDefault(x => x.Id == id);
            }

            return a;

        }

        internal List<OrderDbTable> GetOrdersByUserId(int userId)
        {
            List<OrderDbTable> orders;
            using (var db = new OrderContext())
            {
                orders = db.Order.Where(x => x.UserId == userId).ToList();
            }
            return orders;

        }

        internal bool UpdateOrderStatus(int orderId, string newStatus)
        {
            using (var db = new OrderContext())
            {
                var order = db.Order.FirstOrDefault(x => x.Id == orderId);
                if (order == null) return false;

                order.Status = newStatus;
                db.SaveChanges();
                return true;
            }
        }

        internal List<OrderDbTable> GetAllOrders()
        {
            using (var db = new OrderContext())
            {
                return db.Order.ToList();
            } 
        }

        internal List<OrderDbTable> SearchOrders(string searchTerm)
        {
            using (var db = new OrderContext())
            {
                return db.Order
                    .Where(x => x.Id.ToString().Contains(searchTerm) ||
                                x.UserId.ToString().Contains(searchTerm))
                    .ToList();
            }
        }

        internal OrderDbTable GetOrderDetails(int orderId)
        {
            using (var db = new OrderContext())
            {
                return db.Order.FirstOrDefault(x => x.Id == orderId);
            }
        }

        internal int GetOrderCountByStatus(string status)
        {
            using (var db = new OrderContext())
            {
                return db.Order.Count(x => x.Status == status);
            }
        }

    }
}
/*Null if error in controller OrderByIdACtion*/
