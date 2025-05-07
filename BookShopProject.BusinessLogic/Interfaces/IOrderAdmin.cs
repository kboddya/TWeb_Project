using System;
using BookShopProject.Domain.Entities.Order;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IOrderAdmin
    {
        OrderDbTable GetOrderById(int id);

        OrdersList GetOrders();
    }
}
