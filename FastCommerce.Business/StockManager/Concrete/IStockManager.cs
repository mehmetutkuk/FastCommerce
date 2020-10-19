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
        Task<List<StockPropertyCombination>> GetStocks(int page);
        Task<List<GetStocksDto>> GetStocks();
    }
}
