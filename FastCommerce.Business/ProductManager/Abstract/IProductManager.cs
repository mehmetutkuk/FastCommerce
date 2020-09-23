using FastCommerce.Business.DTOs.Product;
using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastCommerce.Business.ProductManager.Abstract
{
    public interface IProductManager
    {
        Task<bool> CreateIndexes(ProductElasticIndexDto productElasticIndexDto);
        Task<List<ProductGetDTO>> Get();
        ProductGetDTO GetProductById(int ProdcutId);
        Task<List<Product>> GetByCategories(GetByCategoriesRequest req);
        Task<bool> AddProduct(Product product);
        Task<List<ProductElasticIndexDto>> SuggestProductSearchAsync(string searchText, int skipItemCount = 0, int maxItemCount = 5);
    }
}
