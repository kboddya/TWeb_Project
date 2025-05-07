using System;
using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Order;

namespace BookShopProject.BusinessLogic
{
    public class OrderAdminBL : AdminApi, IOrderAdmin
    {
        public bool UpdateOrderStatus(int Id, Enum newStatus)
        {
            return UpdateOrderStatusAction(Id, newStatus);
        }

        public OrderDbTable GetOrderById(int id)
        {
            return OrderByIdAction(id);
        }

        public OrdersList GetOrders()
        {
            return OrdersListAction();
        }
    }
}
