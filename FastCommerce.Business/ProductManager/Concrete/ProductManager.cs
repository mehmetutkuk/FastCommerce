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
using FastCommerce.Business.DTOs.Product;
using FastCommerce.Business.Core;
using FastCommerce.Business.ProductManager.Abstract;
using Mapster;
using Newtonsoft.Json;
using FastCommerce.Business.CategoryManager.Abstract;
using FastCommerce.Business.DTOs.Stock;
using Elasticsearch.Net;
using FastCommerce.Business.StockManager.Concrete;
using System.Diagnostics.Tracing;
using FastCommerce.Entities.Models.Enums;
using FastCommerce.Business.DTOs.Property;

namespace FastCommerce.Business.ProductManager.Concrete
{
    public class ProductManager : IProductManager
    {
        private readonly dbContext _context;
        private readonly IElasticSearchService _elasticSearchService;
        private IPropertyManager _propertyManager;
        private ICategoryManager _categoryManager;
        private IStockManager _stockManager;
        public ProductManager(dbContext context, IElasticSearchService elasticSearchService, IPropertyManager propertyManager,
            ICategoryManager categoryManager, IStockManager stockManager)
        {
            _context = context;
            _elasticSearchService = elasticSearchService;
            _propertyManager = propertyManager;
            _categoryManager = categoryManager;
            _stockManager = stockManager;
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

        public async Task<List<ProductGetDTO>> GetProductsByCategoryId(int id) => _context.Products.Where(p => p.ProductCategories.Any(pc => pc.CategoryId == id)).Select(_ => _.Adapt<ProductGetDTO>()).ToList();

        public async Task<List<ProductGetDTO>> GetProductsByCategoryName(string name) => _context.Products.Where(p => p.ProductCategories.Any(pc => pc.Category.CategoryName == name)).Select(_ => _.Adapt<ProductGetDTO>()).ToList();

        public async Task<List<ProductGetDTO>> Get()
        {

            return _context.Products.Select(pr => new ProductGetDTO
            {
                ProductId = pr.ProductId,
                Discount = pr.Discount,
                Price = pr.Price,
                ProductName = pr.ProductName,
                Rating = pr.Rating,
                ProductImages = _context.ProductImages.Where(c => c.ProductId == pr.ProductId).OrderBy(r => r.ProductImagesId).ToList()
            }).ToList();

        }
        public async Task<ProductGetDTO> GetProductById(int id) => _context.Products.Where(wh => wh.ProductId == id).Select(sel => new ProductGetDTO
        {
            ProductId = sel.ProductId,
            Discount = sel.Discount,
            Price = sel.Price,
            ProductName = sel.ProductName,
            Rating = sel.Rating,
            ProductImages = _context.ProductImages.Where(c => c.ProductId == id).ToList()
        }
        ).SingleOrDefault().Adapt<ProductGetDTO>();


        public async Task<bool> AddProduct(AddProductDto productdto)
        {
            Product adedproduct = productdto.Adapt<Product>();
            await _context.Products.AddAsync(adedproduct);
            await _context.SaveChangesAsync();

            if (!await _categoryManager.AddProductCategoryRelation(productdto.CategoryId, adedproduct.ProductId))
                throw new Exception("Product And Category Relation Requried");

            if (!await _stockManager.SetStockPropertyCombination(productdto.CategoryId, adedproduct.ProductId))
                throw new Exception("Product Variation Requried");

            if (!await AddProductImages(productdto.Images, adedproduct.ProductId))
                throw new Exception("Product Images Requried");

            //ProductElasticIndexDto productElasticIndexDto = new ProductElasticIndexDto();
            //productElasticIndexDto.Adapt(adedproduct);
            //await CreateIndexes(productElasticIndexDto);
            return await Task.FromResult<bool>(true);
        }

        public async Task<bool> AddProductImages(List<AddProductImages> images, int ProductId)
        {
            foreach (var image in images)
            {
                await _context.ProductImages.AddAsync(new ProductImage { ImageURL = image.Img, ProductId = ProductId });
            }
            await _context.SaveChangesAsync();
            return await Task.FromResult<bool>(true);
        }
        public async Task<List<GetTrendingProductsDto>> GetTrendingProducts()
        {
            var trendingProducts = await _context.TrendingProducts.Include(tp => tp.Product).ThenInclude(product => product.ProductImages).ToListAsync();

            var trendingCategories = from tp in trendingProducts
                                     group tp by new { tp.CategoryName }
                into gtpd
                                     select new { gtpd.Key.CategoryName };

            var getTrendingProductsDtoList = new List<GetTrendingProductsDto>();
            foreach (var item in trendingCategories)
            {
                var productGetDtoList = trendingProducts.Where(tp => tp.CategoryName == item.CategoryName)
                    .OrderBy(tp => tp.DisplayOrder)
                    .Select(tp => tp.Product.Adapt<ProductGetDTO>()).ToList();
                var getTrendingProductsDto = new GetTrendingProductsDto()
                {
                    CategoryName = item.CategoryName,
                    Products = productGetDtoList
                };
                getTrendingProductsDtoList.Add(getTrendingProductsDto);
            }
            return getTrendingProductsDtoList;
        }

        public async Task<bool> AddTrendingProduct(TrendingProduct trendingProduct)
        {
            await _context.AddAsync(trendingProduct);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<List<TrendingProduct>> GetTrendingProductEntities() =>
            await _context.TrendingProducts.Include(_ => _.Product).ToListAsync();
        public async Task<bool> UpdateTrendingProduct(TrendingProduct trendingProduct)
        {
            var trendingProductEntities = await _context.TrendingProducts
                .Where(tp => tp.CategoryName == trendingProduct.CategoryName).ToListAsync();
            foreach (var item in trendingProductEntities)
            {
                item.CategoryName = trendingProduct.CategoryName;
            }

            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<bool> RemoveTrendingProduct(RemoveTrendingProductDto trendingProduct)
        {
            _context.Remove(trendingProduct.Adapt<TrendingProduct>());
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<List<ProductGetDTO>> GetProductByPageNumber(PostProductDTO payload)
        {
            int[] filteredProductIds = arrayFilterSelector(getProductIdByCategories(payload.categoryIds), getProductIdByPropertyDetails(payload.propertyDetailIds));

            var query = _context.Products.Include("ProductImages")
                   .OrderByDescending(d => d.LastModified).Skip(payload.pageNo * payload.pageSize);
            if (filteredProductIds != null)
                query = query.Where(p => filteredProductIds.Contains(p.ProductId));

            query = query.Take(payload.pageSize);

            List<ProductGetDTO> result = await query.Select(pr => pr.Adapt<ProductGetDTO>()).ToListAsync();
            return await Task.FromResult<List<ProductGetDTO>>(result);
        }
        public int[] arrayFilterSelector(int[] a, int[] b)
        {
            if (a.Length > 0 & b.Length > 0)
            {
                return a.Intersect(b).ToArray();
            }
            else if (a.Length > 0 & b.Length == 0)
            { return a; }
            else if (b.Length > 0 & a.Length == 0)
            { return b; }
            else
            {
                return null;
            }
        }

        public int[] getProductIdByCategories(int[] categoryIds)
        {
            return _context.ProductCategories.Where(s => categoryIds.Contains(s.CategoryId)).Select(a => a.ProductId).ToArray();
        }
        public int[] getProductIdByPropertyDetails(int[] propertyDetailIds)
        {
            return _context.StockPropertyCombinations.Include("Stock").Where(s => propertyDetailIds.Contains(s.PropertyDetailId))
                .Select(a => a.Stock.ProductId).ToArray();
        }

        public async Task<GetProductFilters> GetProductFilters()
        {
            return await Task.FromResult<GetProductFilters>(new GetProductFilters()
            {
                productCategoryFilters = _context.Category
                .Select(s => new Category { CategoryId = s.CategoryId, CategoryName = s.CategoryName }).ToList(),

                productPropertyFilters = (await _context.Properties
                .Include("PropertyDetails")
                .ToListAsync()).GroupBy(u => u.PropertyName).Select(grp => grp.ToList())
            });

        }

        public async Task<GetMinMaxPriceDto> GetMinMaxPrice()
        {

            return (from row in _context.Products
                    group row by true into r
                    select new GetMinMaxPriceDto
                    {
                        Min = r.Min(z => z.Price),
                        Max = r.Max(z => z.Price)
                    }).SingleOrDefault();

        }
    }

}
