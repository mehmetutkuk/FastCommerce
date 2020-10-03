using FastCommerce.Business.DTOs.Order;
using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastCommerce.Business.OrderManager.Abstract
{
    public interface IOrderManager
    {
        Task<bool> CreateIndexes(OrderElasticIndexDto orderElasticIndexDto);
        Task<bool> AddOrder(Order order);
        Task<bool> DeleteOrder(Order OrderId);
        Task<bool> UpdateOrder(int OrderId);
        OrderGetDTO GetOrdersByUser(int UserId);
        Task<List<OrderGetDTO>> GetOrders();
    }
}
