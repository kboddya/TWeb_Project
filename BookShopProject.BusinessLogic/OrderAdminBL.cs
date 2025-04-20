using BookShopProject.BusinessLogic.Core;
using BookShopProject.BusinessLogic.Interfaces;
using BookShopProject.Domain.Entities.Order;
using System.Collections.Generic;

public class OrderAdminBL : AdminApi, IOrder
{
    public bool UpdateOrder(OrderDbTable order)
    {
        return UpdateOrderStatus(order.Id, order.Status);
    }

    public OrderDbTable OrderById(int id)
    {
        return OrderByIdAction(id);
    }

    public List<OrderDbTable> GetOrders()
    {
        return GetAllOrders();
    }

    public List<OrderDbTable> GetOrdersByUserId(int userId)
    {
        return base.GetOrdersByUserId(userId);
    }

    public bool UpdateOrderStatus(int orderId, string newStatus)
    {
        return base.UpdateOrderStatus(orderId, newStatus);
    }

    public List<OrderDbTable> GetAllOrders()
    {
        return base.GetAllOrders();
    }

    public List<OrderDbTable> SearchOrders(string searchTerm)
    {
        return base.SearchOrders(searchTerm);
    }

    public OrderDbTable GetOrderDetails(int orderId)
    {
        return base.GetOrderDetails(orderId);
    }

    public int GetOrderCountByStatus(string status)
    {
        return base.GetOrderCountByStatus(status);
    }
}
