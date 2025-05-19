using System;
using BookShopProject.Domain.Entities.Book;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IOrderUser
    {
        OrdersList GetOrders(int userId);
        OrderDbTable GetOrderById(int id);
        bool AddCart(OrderDbTable cart);
        bool DeleteCart(OrderDbTable order);
        decimal CountPrice(int userId);
        bool BuyCart(int userId);

    }
}