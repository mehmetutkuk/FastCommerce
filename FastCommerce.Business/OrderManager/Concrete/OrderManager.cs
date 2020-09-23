using FastCommerce.Business.OrderManager.Abstract;
using FastCommerce.DAL;
using FastCommerce.Entities.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCommerce.Business.OrderManager.Concrete
{
    public class OrderManager : IOrderManager
    {
        private readonly dbContext _context;
        public OrderManager(dbContext context)
        {
            _context = context;
        }


        public async Task<bool> AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return await Task.FromResult<bool>(true);
        }

        public async Task<bool> DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return await Task.FromResult<bool>(true);
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            Order result = _context.Orders.Where(o => o.OrderId == order.OrderId).SingleOrDefault();
            result.Adapt(order);
            await _context.SaveChangesAsync();
            return await Task.FromResult<bool>(true);
        }

        public async Task<List<Order>> GetOrdersByUser(int UserId)
        {
            List<Order>  orders =  _context.Orders.Where(c => c.UserId == UserId).ToList();
            return await Task.FromResult<List<Order>>(orders);
        }

        public async Task<List<Order>> GetOrders()
        {
            List<Order> orders = _context.Orders.Select(c => c).ToList();
            return await Task.FromResult<List<Order>>(orders);
        }
    }
}
