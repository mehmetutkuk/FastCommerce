using FastCommerce.Business.Core;
using FastCommerce.Business.DTOs.Order;
using FastCommerce.Business.ElasticSearch.Abstract;
using FastCommerce.Business.OrderManager.Abstract;
using FastCommerce.DAL;
using FastCommerce.Entities.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastCommerce.Business.OrderManager.Concrete
{
    public class OrderManager : IOrderManager
    {
        private readonly dbContext _context;
        private readonly IElasticSearchService _elasticSearchService;
        public OrderManager(dbContext context, IElasticSearchService elasticSearchService)
        {
            _context = context;
            _elasticSearchService = elasticSearchService;
        }

        public async Task<bool> CreateIndexes(OrderElasticIndexDto orderElasticIndexDto)
        {
            try
            {
                await _elasticSearchService.CreateIndexAsync<OrderElasticIndexDto, int>(ElasticSearchItemsConst.OrderIndexName);
                await _elasticSearchService.AddOrUpdateAsync<OrderElasticIndexDto, int>(ElasticSearchItemsConst.OrderIndexName, orderElasticIndexDto);
                return await Task.FromResult<bool>(true);
            }
            catch (Exception ex)
            {
                return await Task.FromException<bool>(ex);
            }
        }

        public async Task<bool> AddOrder(Order order)
        {
            await _context.AddAsync<Order>(order);
            await _context.SaveChangesAsync();
            OrderElasticIndexDto orderElasticIndexDto = new OrderElasticIndexDto();
            orderElasticIndexDto.Adapt(order);
            await CreateIndexes(orderElasticIndexDto);
            return await Task.FromResult<bool>(true);
        }

        public async Task<bool> DeleteOrder(Order OrderId)
        {
            Order result = await _context.Orders.FindAsync(OrderId);
            _context.Orders.Remove(result);
            await _context.SaveChangesAsync();
            return await Task.FromResult<bool>(true);
        }

        public Task<bool> UpdateOrder(int OrderId)
        {
            Order result = _context.Orders.Where(o => o.OrderId == OrderId).SingleOrDefault();
            result.Adapt(OrderId);
            _context.SaveChangesAsync();
            return Task.FromResult<bool>(true);
        }

        public OrderGetDTO GetOrdersByUser(int UserId) => _context.Orders.Where(wh => wh.UserId == UserId).Select(sel => new OrderGetDTO
        {
            OrderId = sel.OrderId,
            UserId = sel.UserId,
            Stage = sel.Stage,
            Shipment = sel.Shipment,
            OrderProducts = _context.OrderProducts.Where(c => c.Order.UserId == UserId).ToList()
        }).SingleOrDefault().Adapt<OrderGetDTO>();

        public async Task<List<OrderGetDTO>> GetOrders()
        {
            List<OrderGetDTO> orders = _context.Orders.Select(pr => new OrderGetDTO
            {
                OrderId = pr.OrderId,
                UserId = pr.UserId,
                Stage = pr.Stage,
                Shipment = pr.Shipment,
                OrderProducts = _context.OrderProducts.Where(c => c.OrderId == pr.OrderId).ToList()
            }).ToList();
            return orders;
        }
    }
}