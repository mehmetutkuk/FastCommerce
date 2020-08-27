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

namespace FastCommerce.Business.ProductManager
{
    public class ProductManager : IProductManager
    {
        private readonly dbContext _context;
        public ProductManager(dbContext context)
        {
            _context = context;
        }
        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }
    }
    public interface IProductManager
    {
        public List<Product> GetProducts();
    }
}
