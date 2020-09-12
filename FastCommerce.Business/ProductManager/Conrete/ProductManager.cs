using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FastCommerce.Entities.Entities;
using FastCommerce.DAL;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FastCommerce.Business.ElasticSearch.Abstract;
using FastCommerce.Business.ObjectDtos.Product;
using FastCommerce.Business.Core;
using FastCommerce.Business.ProductManager.Abstract;
using Mapster;

namespace FastCommerce.Business.ProductManager.Conrete
{
    public class ProductManager : IProductManager
    {
        private readonly dbContext _context;
        private readonly IElasticSearchService _elasticSearchService;
        public ProductManager(dbContext context, IElasticSearchService elasticSearchService)
        {
            _context = context;
            _elasticSearchService = elasticSearchService;

        }
        public async Task<bool> CreateIndexes(ProductElasticIndexDto productElasticIndexDto)
        {
            try
            {
                await _elasticSearchService.CreateIndexAsync<ProductElasticIndexDto, int>(ElasticSearchItemsConst.ProductIndexName);
                await _elasticSearchService.AddOrUpdateAsync<ProductElasticIndexDto, int>(ElasticSearchItemsConst.ProductIndexName, productElasticIndexDto);
                return await Task.FromResult<bool>(true);
            }
            catch (Exception ex)
            {
                return await Task.FromException<bool>(ex);
            }

        }
        public async Task<List<Product>> GetByCategories(GetByCategoriesRequest req)
        {
            return await _context.Products
                .Where(p => p.ProductCategories.All(item => req.Categories.Contains(item.Category))).ToListAsync();
        }
        public async Task<List<Product>> Get()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<Product> AddProduct(Product product)
        {
            await _context.AddAsync<Product>(product);
            await _context.SaveChangesAsync();
            ProductElasticIndexDto productElasticIndexDto = new ProductElasticIndexDto();
            productElasticIndexDto.Adapt(product);
            await CreateIndexes(productElasticIndexDto);
            return await Task.FromResult<Product>(product);
        }
    }
}

