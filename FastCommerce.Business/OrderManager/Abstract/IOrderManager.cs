using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FastCommerce.Business.DTOs.Order;

namespace FastCommerce.Business.OrderManager.Abstract
{
    public interface IOrderManager
    {
        Task<bool> AddOrder(Order order);
        Task<bool> CreateOrder(CreateOrderDto order);
        Task<bool> DeleteOrder(Order order);
        Task<bool> UpdateOrder(Order order);
        Task<List<Order>> GetOrdersByUser(int UserId);
        Task<List<Order>> GetOrders();
    }
}
