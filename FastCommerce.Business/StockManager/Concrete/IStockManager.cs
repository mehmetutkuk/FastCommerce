using FastCommerce.Business.DTOs.Stock;
using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastCommerce.Business.StockManager.Concrete
{
    public interface IStockManager
    {
        Task<List<GetStocksDto>> GetStocksByProductId(int id);
        Task<List<GetStocksDto>> GetStocks();
        Task<bool> SetStockPropertyCombination(int CategoryId, int ProductId);
        Task<bool> UpdateQuantityByStockId(int Id, int Quantity);
    }
}
