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
using Newtonsoft.Json;

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



        public async Task<List<ProductElasticIndexDto>> SuggestProductSearchAsync(string searchText, int skipItemCount = 0, int maxItemCount = 5)
        {
            try
            {
                var indexName = ElasticSearchItemsConst.ProductIndexName;
                var queryy = new Nest.SearchDescriptor<ProductElasticIndexDto>()  // SearchDescriptor burada oluşturacağız 
                              .Suggest(su => su
                                               .Completion("product_suggestions",
                                      c => c.Field(f => f.Suggest)
                                               .Analyzer("simple")
                                               .Prefix(searchText)
                                               .Fuzzy(f => f.Fuzziness(Nest.Fuzziness.Auto))
                                               .Size(10))
                                        );

                var returnData = await _elasticSearchService.SearchAsync<ProductElasticIndexDto, int>(indexName, queryy, 0, 0);

                var data = JsonConvert.SerializeObject(returnData);
                
                var suggestsList = returnData.Suggest != null ? from suggest in returnData.Suggest["product_suggestions"]
                                                                  from option in suggest.Options
                                                                  select new ProductElasticIndexDto
                                                                  {
                                                                      Score = option.Score,
                                                                      CategoryName = option.Source.CategoryName,
                                                                      ProductName = option.Source.ProductName,
                                                                      Suggest = option.Source.Suggest,
                                                                      Id = option.Source.Id
                                                                  }
                                                                  : null;
                return await Task.FromResult(suggestsList.ToList());
            }
            catch (Exception ex)
            {
                return await Task.FromException<List<ProductElasticIndexDto>>(ex);
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
        public Product GetProductById(int ProdcutId)
        {
            return _context.Products.Where(c => c.ProductId == ProdcutId).Select(s => s).SingleOrDefault();
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

