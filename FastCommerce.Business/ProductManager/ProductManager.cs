using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FastCommerce.Entities.Entities;
using FastCommerce.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using FastCommerce.Entities.Models;

namespace FastCommerce.Business.ProductManager
{
    public class ProductManager : IProductManager
    {
        private readonly dbContext _context;
        public ProductManager(dbContext context)
        {
            _context = context;
        }
        public List<Product> Get()
        {
            return _context.Products.ToList();
        }

        public void SetPlaces(SetPlacesRequest req)
        {
            Product product = _context.Products
                .Where(p => p.ProductId == req.ProductId).FirstOrDefault();
            product.PlacementIds = req.PlacementIds;
            _context.SaveChangesAsync();
        }
        public List<Product> GetByPlaces(GetByPlacesRequest req)
        {
            List<Product> products = _context.Products.Where(w => w.PlacementIds.All(q => req.PlacementIds.Contains(q))).ToList();
            return products;
        }
    }
    public interface IProductManager
    {
        List<Product> Get();
        void SetPlaces(SetPlacesRequest req);
        List<Product> GetByPlaces(GetByPlacesRequest req);
    }
}
