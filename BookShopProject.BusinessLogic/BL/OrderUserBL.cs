using System;
using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Book;

namespace BookShopProject.BusinessLogic
{
    public class OrderUserBL : UserApi, IOrderUser
    {
        public OrdersList GetOrders(int userId)
        {
            return OrdersListAction(userId);
        }

        public OrdersList GetPurchased(int userId)
        {
            return PurchaseListAction(userId);
        }

        public OrderDbTable GetOrderById(int id)
        {
            return OrderByIdAction(id);
        }

        public bool AddCart(OrderDbTable cart)
        {
            return AddCartAction(cart);
        }

        public bool DeleteCart(OrderDbTable order)
        {
            return DeleteCartAction(order);
        }

        public decimal CountPrice(int userId)
        {
            return CountPriceAction(userId);
        }

        public bool BuyCart(int userId)
        {
            return BuyCartAction(userId);
        }
    }
}