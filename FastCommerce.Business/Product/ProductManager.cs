using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FastCommerce.Entities.Entities;
namespace FastCommerce.Business.Product
{
    public static class ProductManager
    {
        private static IElasticClient _elasticClient { get; set; }
        public static void Product(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        public static async Task SaveProduct(Entities.Entities.Product product)
        {
            string filePath = "";
            product.LastModified = DateTime.UtcNow;

            bool productExists = File.Exists(filePath);

            if (productExists)
                await _elasticClient.UpdateAsync<Entities.Entities.Product>(product, u => u.Doc(product));
            else
                await _elasticClient.IndexDocumentAsync(product);
        }
    }
}
