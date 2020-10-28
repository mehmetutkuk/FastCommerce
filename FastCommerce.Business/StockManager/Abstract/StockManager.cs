using FastCommerce.Business.CategoryManager.Abstract;
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
        private IPropertyManager _propertyManager;
        public StockManager(dbContext context, IPropertyManager propertyManager)
        {
            _context = context;
            _propertyManager = propertyManager;
        }

        public async Task<List<GetStocksDto>> GetStocksByProductId(int id)
        {
            List<GetStocksDto> getStocksDtosList = new List<GetStocksDto>();

            var Stocks = (from sp in _context.StockPropertyCombinations
                          where sp.Stock.ProductId == id
                          group sp by new { sp.StockId } into gr
                          select gr.Key
            ).ToArray();

            foreach (var item in Stocks)
            {
                List<PropertyDetail> propertyDetails = _context.StockPropertyCombinations.Include("PropertyDetail").Where(c => c.StockId == item.StockId).Select(s => s.PropertyDetail).ToList();
                Product StockProduct = _context.Stocks
                    .Include("Product")
                    .Where(c => c.StockId == item.StockId)
                    .Select(s => s.Product).FirstOrDefault();

                List<ProductImage> productImages = _context.ProductImages.Where(s => s.ProductId == StockProduct.ProductId).ToList();

                int Quantity = _context.Stocks.Where(s => s.StockId == item.StockId).Select(q => q.Quantity).FirstOrDefault();

                getStocksDtosList.Add(new GetStocksDto
                {
                    PropertyDetails = propertyDetails,
                    ProductId = StockProduct.ProductId,
                    ProductName = StockProduct.ProductName,
                    ProductImages = productImages,
                    Quantity = Quantity
                });
            }
            return await Task.FromResult<List<GetStocksDto>>(getStocksDtosList);
        }
        public async Task<List<GetStocksDto>> GetStocks()
        {
            List<GetStocksDto> getStocksDtosList = new List<GetStocksDto>();


            var Stocks = (from sp in _context.StockPropertyCombinations
                          group sp by new { sp.StockId} into gr
                          select gr.Key
            ).ToArray();

            foreach (var item in Stocks)
            {
                List<PropertyDetail> propertyDetails = _context.StockPropertyCombinations.Include("PropertyDetail").Where(c => c.StockId == item.StockId).Select(s => s.PropertyDetail).ToList();
             
                
                Product StockProduct = _context.Stocks
                    .Include("Product")
                    .Where(c => c.StockId== item.StockId)
                    .Select(s =>  s.Product).FirstOrDefault();

                List<ProductImage> productImages = _context.ProductImages.Where(s => s.ProductId == StockProduct.ProductId).ToList();

                int Quantity =  _context.Stocks.Where(s => s.StockId == item.StockId).Select(q => q.Quantity).FirstOrDefault();
                
                getStocksDtosList.Add(new GetStocksDto
                {
                    PropertyDetails = propertyDetails,
                    ProductId = StockProduct.ProductId,
                    ProductName = StockProduct.ProductName,
                    StockId = item.StockId,
                    ProductImages = productImages,
                    Quantity = Quantity
                });
            }
            return await Task.FromResult<List<GetStocksDto>>(getStocksDtosList);
        }

        public async Task<bool> SetStockPropertyCombination(int CategoryId, int ProductId)
        {
            List<Property> categoryProperties = await _propertyManager.GetPropertiesByCategoryId(CategoryId);
            List<StockPropertyCombination> listOfStockPropertyCombination = new List<StockPropertyCombination>();
            List<List<PropertyDetail>> listofPropertyDetails = new List<List<PropertyDetail>>();

            foreach (var cpRow in categoryProperties)
            {
                listofPropertyDetails.Add(_context.PropertyDetails
                  .Where(c => c.PropertyId == cpRow.PropertyID).AsNoTracking()
                  .ToList());
            }

            var CombinatedProductPropertyDetialsList = CartesianProduct<PropertyDetail>(listofPropertyDetails);

            foreach (var Combination in CombinatedProductPropertyDetialsList)
            {
                Stock Stock = new Stock
                {
                    Quantity = 0,
                    ProductId = ProductId,
                };

                foreach (var item in Combination)
                {
                    listOfStockPropertyCombination.Add(new StockPropertyCombination
                    {
                        PropertyDetailId = item.PropertyDetailId,
                        Stock = Stock
                    });
                }
            }

            await _context.StockPropertyCombinations.AddRangeAsync(listOfStockPropertyCombination);

            return await Task.FromResult<bool>(true);
        }
        public IEnumerable<IEnumerable<T>> CartesianProduct<T>(List<List<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> result = new[] { Enumerable.Empty<T>() };
            foreach (var sequence in sequences)
            {
                var localSequence = sequence;
                result = result.SelectMany(
                  _ => localSequence,
                  (seq, item) => seq.Concat(new[] { item })
                );
            }
            return result;
        }
        List<StockPropertyCombination> GetCombos(IEnumerable<KeyValuePair<int, List<PropertyDetail>>> remainingTags, Stock stock)
        {


            if (remainingTags.Count() == 1)
            {
                List<StockPropertyCombination> list = new List<StockPropertyCombination>();

                foreach (PropertyDetail pd in remainingTags.First().Value)
                {
                    list.Add(new
                StockPropertyCombination
                    {
                        Stock = new Stock
                        {
                            ProductId = stock.ProductId,
                            Quantity = 0
                        },
                        PropertyDetailId = pd.PropertyDetailId
                    });
                }
                return list;
            }
            else
            {
                var current = remainingTags.First();
                List<StockPropertyCombination> outputs = new List<StockPropertyCombination>();

                List<StockPropertyCombination> combos = GetCombos(remainingTags.Where(tag => tag.Key != current.Key), stock);

                foreach (var tagPart in current.Value)
                {
                    foreach (var combo in combos)
                    {
                        stock = new Stock
                        {
                            Quantity = 0,
                            ProductId = stock.ProductId
                        };

                        outputs.Add(new StockPropertyCombination
                        {
                            Stock = stock,
                            PropertyDetailId = tagPart.PropertyDetailId
                        });

                        outputs.Add(new StockPropertyCombination
                        {
                            Stock = stock,
                            PropertyDetailId = combo.PropertyDetailId
                        });
                    }
                }

                return outputs;
            }


        }

        public async Task<bool> UpdateQuantityByStockId(int Id,int Quantity)
        {
            Stock stock = _context.Stocks.Where(s => s.StockId == Id).FirstOrDefault();
            stock.Quantity = Quantity;
            await _context.SaveChangesAsync();

            return await Task.FromResult<bool>(true);
        }
    }
}
