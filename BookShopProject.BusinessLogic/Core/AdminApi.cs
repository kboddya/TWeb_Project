using System;
using System.Collections.Generic;
using System.Linq;
using BookShopProject.Domain.Entities.Order;

namespace BookShopProject.BusinessLogic.Core
{
    public class AdminApi
    {

        internal OrdersList OrdersListAction()
        {
            var a = new OrdersList();
            using (var db = new OrderContext())
            {
                a.Orders = db.Orders.ToList();
            }

            return a;
        }

        internal bool AddOrderAction(OrderDbTable order)
        {
            using (var db = new OrderContext())
            {
                if (db.Orders.FirstOrDefault(x => x.Id == order.Id) != null) return false;
                db.Orders.Add(order);
                db.SaveChanges();
                return true;
            }
        }

        internal bool UpdateOrderStatusAction(int Id, Enum newStatus)
        {
            using (var db = new OrderContext())
            {
                var order = db.Orders.FirstOrDefault(x => x.Id == Id);
                if (order == null) return false;

                order.Status = newStatus;
                order.LastUpdateTime = DateTime.Now;
                db.SaveChanges();
                return true;
            }
        }

        internal OrderDbTable OrderByIdAction(int id)
        {
            OrderDbTable a;
            using (var db = new OrderContext())
            {
                a = db.Orders.FirstOrDefault(x => x.Id == id);
            }

            return a;
        }
    }
}
/*Null if error in controller OrderByIdACtion*/
