using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FastCommerce.Entities.Entities;
using FastCommerce.Entities.Models;
using FastCommerce.DAL;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FastCommerce.Business.ProductManager
{
    public class ProductManager : IProductManager
    {
        public void SetPlaces(SetPlacesRequest req)
        {
            Product product = _context.Products
                .Where(p => p.ProductId == req.ProductId).FirstOrDefault();
            _context.SaveChangesAsync();
        }
        public List<Product> GetByCategories(GetByCategoriesRequest req)
        {
                .Where(p => p.Categories.All(item => req.Categories.Contains(item))).ToList();
        }
            return _context.Products
        private readonly dbContext _context;
        private static readonly ElasticClient elasticClient;

        public ProductManager(dbContext context)
        {
            _context = context;
        }
        public void Indexing()
        {
     
                var Products = _context.Products.Where(s => s.Quantity != 0)
                .Include(s => s.Category.Categoryname)
                .Select(s => new Product
                {
                    ProductName = s.ProductName,     
                }).ToList();


                var defaultIndex = "product_index";
                var client = new ElasticClient();

                if (elasticClient.IndexExists(defaultIndex).Exists)
                {
                    client.DeleteIndex(defaultIndex);
                }

                if (!elasticClient.IndexExists("location_alias").Exists)
                {
                    client.CreateIndex(defaultIndex, c => c
                        .Mappings(m => m
                            .Map<CustomerModel>(mm => mm
                                .AutoMap()
                            )
                        ).Aliases(a => a.Alias("location_alias"))
                    );
                }

                // Insert Data Classic
                // for (int i = 0; i < customerLocationList.Count; i++)
                //     {
                //         var item = customerLocationList[i];
                //         elasticClient.Index<CustomerModel>(item, idx => idx.Index("customerlocation").Id(item.LocationId));
                //     }

                // Bulk Insert
                var bulkIndexer = new BulkDescriptor();

                foreach (var document in customerLocationList)
                {
                    bulkIndexer.Index<Product>(i => i
                        .Document(document)
                        .Id(document.LocationId)
                        .Index("product"));
                }

                client.Bulk(bulkIndexer);
            }
        }
        public List<Product> Get()
        {
            return _context.Products.ToList();
        }
    }
    public interface IProductManager
    {
        List<Product> Get();
        void SetPlaces(SetPlacesRequest req);
        List<Product> GetByCategories(GetByCategoriesRequest req);
    }
}
