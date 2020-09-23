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

namespace FastCommerce.Business.CategoryManager.Concrete
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
            var result = _context.Properties.Single(w => w.PropertyID == property.PropertyID);
            result.Adapt(property);
            _context.SaveChanges();
            return await Task.FromResult<bool>(true);
        }

        public async Task<List<Property>> GetPropertiesByCategoryId(int CategoryId)
        {
            List<Property> result = _context.Properties.Where(c => c.CategoryId == CategoryId).ToList();
            return await Task.FromResult<List<Property>>(result);
        }
        public async Task<Property> GetPropertyById(int Id)
        {
            Property result = _context.Properties.Single(c => c.PropertyID == Id);
            return await Task.FromResult<Property>(result);
        }
        public async Task<List<Property>> GetProperties()
        {
            return await Task.FromResult<List<Property>>(_context.Properties.Select(s => s).ToList());
        }

    }
}
