using System;
using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Order;

namespace BookShopProject.BusinessLogic
{
    public class OrderAdminBL : AdminApi, IOrderAdmin
    {
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
