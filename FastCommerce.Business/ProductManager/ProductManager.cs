using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FastCommerce.Entities.Entities;
namespace FastCommerce.Business.ProductManager
{
    public static class ProductManager
    {
        public static async Task SaveSingleAsync(Product product)
        {
            if (_cache.Any(p => p.Id == product.Id))
            {
                await _elasticClient.UpdateAsync<Product>(product, u => u.Doc(product));
            }
            else
            {
                _cache.Add(product);
                await _elasticClient.IndexDocumentAsync(product);
            }
        }

        public static async Task SaveManyAsync(Product products)
        {
            _cache.AddRange(products);
            var result = await _elasticClient.IndexManyAsync(products);
            if (result.Errors)
            {
                // the response can be inspected for errors
                foreach (var itemWithError in result.ItemsWithErrors)
                {
                    _logger.LogError("Failed to index document {0}: {1}",
                        itemWithError.Id, itemWithError.Error);
                }
            }
        }

        public static async Task SaveBulkAsync(Product[] products)
        {
            _cache.AddRange(products);
            var result = await _elasticClient.BulkAsync(b => b.Index("products").IndexMany(products));
            if (result.Errors)
            {
                // the response can be inspected for errors
                foreach (var itemWithError in result.ItemsWithErrors)
                {
                    _logger.LogError("Failed to index document {0}: {1}",
                        itemWithError.Id, itemWithError.Error);
                }
            }
        }
    }
}
