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
            _context.SaveChangesAsync();
        }
        public List<Product> GetByCategories(GetByCategoriesRequest req)
        {
            return _context.Products
                .Where(p => p.Categories.All(item => req.Categories.Contains(item))).ToList();
        }
    }
    public interface IProductManager
    {
        List<Product> Get();
        void SetPlaces(SetPlacesRequest req);
        List<Product> GetByCategories(GetByCategoriesRequest req);
    }
}
