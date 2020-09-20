using FastCommerce.Business.CategoryManager.Abstract;
using FastCommerce.DAL;
using FastCommerce.Entities.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCommerce.Business.CategoryManager.Conrete
{
   public class PropertyManager: IPropertyManager
    {
        private readonly dbContext _context;

        public PropertyManager(dbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddProperty(Property property)
        {
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();
            return await Task.FromResult<bool>(true);
        }

        public async Task<bool> DeleteProperty(Property property)
        {
            _context.Properties.Remove(property);
            _context.SaveChanges();
            return await Task.FromResult<bool>(true);
        }

        public async Task<bool> UpdateProperty(Property property)
        {
            var result = _context.Properties.Select(s => s).Where(w => w.PropertyID == property.PropertyID).FirstOrDefault();
            result.Adapt(property);
            _context.SaveChanges();
            return await Task.FromResult<bool>(true);
        }

        public async Task<List<Property>> GetPropertiesByCategoryId(int CategoryId)
        {
            List<Property> result = _context.Properties.Where(c => c.CategoryId == CategoryId).ToList();
            return await Task.FromResult<List<Property>>(result);
        }
        public async Task<Property> GetPropertiesById(int Id)
        {
            Property result = _context.Properties.Where(c => c.PropertyID == Id).SingleOrDefault();
            return await Task.FromResult<Property>(result);
        }
        public async Task<List<Property>> GetProperties()
        {
            return await Task.FromResult<List<Property>>(_context.Properties.Select(s => s).ToList());
        }

    }
}
