using FastCommerce.Business.ElasticSearch.Abstract;
using FastCommerce.Business.ProductManager.Abstract;
using FastCommerce.DAL;
using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FastCommerce.Business.ProductManager.Conrete
{
    public class CategoryManager : ICategoryManager
    {
        private readonly dbContext _context;

        public CategoryManager(dbContext context)
        {
            _context = context;
        }


        public async Task<bool> AddCategory(Category category)
        {
            await _context.AddAsync<Category>(category);
            await _context.SaveChangesAsync();
            return await Task.FromResult<bool>(true);
        }


        public bool DeleteCategory(Category category)
        {
            _context.Remove<Category>(category);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateCategory(Category category)
        {
            var result =  _context.Category.Select(s => s).Where(w => w.CategoryID == category.CategoryID).FirstOrDefault();
            result.CategoryName = category.CategoryName;
            result.Properties = category.Properties;
            _context.SaveChanges();
            return true;
        }



    }
}
