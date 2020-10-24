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

namespace FastCommerce.Business.ProductManager.Concrete
{
    public class ProductManager : IProductManager
    {
        private readonly dbContext _context;
        private readonly IElasticSearchService _elasticSearchService;
        private IPropertyManager _propertyManager;
        private ICategoryManager _categoryManager;
        public ProductManager(dbContext context, IElasticSearchService elasticSearchService, IPropertyManager propertyManager, ICategoryManager categoryManager)
        {
            _context = context;
            _elasticSearchService = elasticSearchService;
            _propertyManager = propertyManager;
            _categoryManager = categoryManager;


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

        //return await _context.Products
        //    .Where(p => p.ProductCategories.All(item => req.Categories.Contains(item.Category))).ToListAsync();
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
                        Stock = stock,
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
                            Product = stock.Product
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

        public async Task<bool> AddProduct(AddProductDto productdto)
        {
            Product adedproduct = productdto.Adapt<Product>();
            await _context.Products.AddAsync(adedproduct);
            await _context.SaveChangesAsync();

            ProductCategories productCategories = new ProductCategories();
            productdto.Adapt(productCategories);
            productCategories.ProductId = adedproduct.ProductId;
            await _context.ProductCategories.AddAsync(productCategories);

     //       List<StockPropertyCombination> stockPropertyCombinations = new List<StockPropertyCombination>();
            #region GetCateogry
            List<Property> categoryProperties = await _propertyManager.GetPropertiesByCategoryId(productdto.CategoryId);


            var CatPropertyCount = categoryProperties.Count;
            Stock Stock = new Stock
            {
                Quantity = 0,
                Product = adedproduct,
            }; ;
            #region CreateStockPropertyCombination

            var listOfStockCombinationDto = new List<StockCombinationDto>();

            foreach (var cpRow in categoryProperties)
            {
                Stock = new Stock
                {
                    Quantity = 0,
                    Product = adedproduct,
                };

                List<PropertyDetail> propertyDetails = _context.PropertyDetails
                  .Where(c => c.PropertyId == cpRow.PropertyID).AsNoTracking()
                  .ToList();

                listOfStockCombinationDto.Add(new StockCombinationDto
                {
                    Key = cpRow.PropertyID,
                    Value = propertyDetails.Select(s => s).ToList()
                });
            }


            List<StockPropertyCombination> rest = GetCombos(listOfStockCombinationDto.Adapt<IEnumerable<KeyValuePair<int, List<PropertyDetail>>>>(), Stock);

            foreach (var item in rest)
            {

                ///_context.Entry<PropertyDetail>(item.PropertyDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                ///_context.Entry<StockPropertyCombination>(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.StockPropertyCombinations.AddAsync(item);
                await _context.SaveChangesAsync();
            }

            

            #endregion

            #endregion
            await _context.SaveChangesAsync();

            #region AddProductImages

            foreach (var image in productdto.Images)
            {
                await _context.ProductImages.AddAsync(new ProductImage { ImageURL = image.Img, ProductId = adedproduct.ProductId });
            }

            await _context.SaveChangesAsync();
            #endregion

            //ProductElasticIndexDto productElasticIndexDto = new ProductElasticIndexDto();
            //productElasticIndexDto.Adapt(adedproduct);
            //await CreateIndexes(productElasticIndexDto);
            return await Task.FromResult<bool>(true);
        }
    }

}
