using FastCommerce.Business.ElasticSearch.Abstract;
using FastCommerce.Business.ProductManager.Abstract;
using FastCommerce.DAL;
using FastCommerce.Entities.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FastCommerce.Business.ProductManager.Concrete
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
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
            return await Task.FromResult<bool>(true);
        }


        public async Task<bool> DeleteCategory(Category category)
        {
            _context.Category.Remove(category);
            _context.SaveChanges();
            return await Task.FromResult<bool>(true);
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            var result = _context.Category.Select(s => s).Single(w => w.CategoryId == category.CategoryId);
            result.Adapt(category);
            _context.SaveChanges();
            return await Task.FromResult<bool>(true);
        }

        public async Task<List<Category>> GetCategories()
        {
            var query = (from cat in _context.Category
                        select new Category { CategoryId=cat.CategoryId, CategoryName = cat.CategoryName }).ToList();

            return await Task.FromResult(query);
        }

    }
}
