using FastCommerce.Business.ObjectDtos.Product;
using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastCommerce.Business.ProductManager.Abstract
{
    public interface IProductManager
    {
        public  Task<bool> CreateIndexes(ProductElasticIndexDto productElasticIndexDto);
        Task<List<Product>> Get();
        Task<List<Product>> GetByCategories(GetByCategoriesRequest req);
    }
}
