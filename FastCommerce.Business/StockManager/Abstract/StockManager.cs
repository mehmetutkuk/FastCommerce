using FastCommerce.Business.DTOs.Stock;
using FastCommerce.Business.StockManager.Concrete;
using FastCommerce.DAL;
using FastCommerce.Entities.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCommerce.Business.StockManager.Abstract
{
    public class StockManager : IStockManager
    {
        private readonly dbContext _context;
        public StockManager(dbContext context)
        {
            _context = context;
        }

        public async Task<List<StockProperties>> GetStocks(int page)
        {
            return await Task.FromResult<List<StockProperties>>(_context.StockProperties
                .Include("Stock")
                .Include("Property")
                .Include("Stock.Product")
                .Include("Stock.Product.ProductImages")
                .Select(s => s).ToList());
        }
        public async Task<List<GetStocksDto>> GetStocks()
        {
            List<GetStocksDto> getStocksDtosList = new List<GetStocksDto>();


            List<StockProperties> stockProperties = _context.StockProperties
                .Include("Stock")
                .Include("Property")
                .Include("Stock.Product")
                .Include("Stock.Product.ProductImages")
                .Select(s => s).ToList();

            foreach (var row in stockProperties)
            {
                int[] CategoryIds = _context.ProductCategories.Where(w => w.Product.ProductId == row.Stock.ProductId).Select(s => s.CategoryId).ToArray();

                GetStocksDto getStocksDto = new GetStocksDto();
                if (CategoryIds.Length > 0)
                    getStocksDto.properties = _context.Properties.Where(c => CategoryIds.Contains(c.CategoryId)).OrderBy(odr => odr.PropertyName)
                        .Select(s=>s.Adapt<GetStocksDtoProperty>()).ToList();

                getStocksDto.ProductName = row.Stock.Product.ProductName;
                getStocksDto.ProductId = row.Stock.Product.ProductId;
                getStocksDto.StockId = row.Stock.StockId;
                getStocksDto.Quantity = row.Stock.Quantity;

                getStocksDto.ProductImages = _context.ProductImages.Where(con => con.ProductId == row.Stock.ProductId).ToList();

                getStocksDtosList.Add(getStocksDto);
            }


            return await Task.FromResult<List<GetStocksDto>>(getStocksDtosList);
        }

    }
}
