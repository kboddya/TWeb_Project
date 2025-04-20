using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopProject.Domain.Entities.Order;

namespace BookShopProject.BusinessLogic.Interfaces
{
    public interface IOrder
    {
        OrderDbTable OrderById(int id);
        List<OrderDbTable> GetOrdersByUserId(int userId);
        bool UpdateOrderStatus(int orderId, string newStatus);
        List<OrderDbTable> GetAllOrders();
        List<OrderDbTable> SearchOrders(string searchTerm);
        OrderDbTable GetOrderDetails(int orderId);
        int GetOrderCountByStatus(string status);
    }
}
