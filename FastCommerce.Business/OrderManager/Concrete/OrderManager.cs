using FastCommerce.Business.OrderManager.Abstract;
using FastCommerce.DAL;
using FastCommerce.Entities.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastCommerce.Business.DTOs.Order;
using FastCommerce.Business.UserManager.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FastCommerce.Business.OrderManager.Concrete
{
    public class OrderManager : IOrderManager
    {
        private readonly dbContext _context;
        public readonly IUserManager _userManager;
        public OrderManager(dbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<bool> AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return await Task.FromResult<bool>(true);
        }
        public async Task<bool> CreateOrder(CreateOrderDto order)
        {
            var newOrder = new Order();
            newOrder.UserId = order.UserId;
            newOrder.OrderProducts = order.Products.Adapt<List<OrderProduct>>();
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

        public async Task<List<AddressDto>> GetAddressesByUser(int UserId) =>
            await _context.Addresses.Where(_ => _.UserId == UserId).Select(_=>_.Adapt<AddressDto>()).ToListAsync();
    }
}
