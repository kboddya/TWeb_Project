using System;
using BookShopProject.Domain.Entities.Book;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IOrderAdmin
    {
        OrderDbTable GetOrderById(int id);

        OrdersList GetOrders();
    }
}
