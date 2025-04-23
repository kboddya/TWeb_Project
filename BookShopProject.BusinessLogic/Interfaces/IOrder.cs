using System;
using BookShopProject.Domain.Entities.Order;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IOrder
    {
        bool UpdateOrderStatus(int Id, Enum Status);

        OrderDbTable GetOrderById(int id);

        OrdersList GetOrders();
    }
}
