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
using FastCommerce.Business.DTOs.Property;

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

        public async Task<bool> AddProperties(List<Property> properties)
        {
            _context.Properties.AddRange(properties);
            await _context.SaveChangesAsync();
            return await Task.FromResult<bool>(true);
        }
        public async Task<bool> AddPropertyByCategoryName(AddPropertyByCategoryNameDto property)
        {
            property.CategoryId = _context.Category.Single(ct => ct.CategoryName == property.CategoryName).CategoryId;

            _context.Properties.AddRange(property.Adapt<Property>());
            await _context.SaveChangesAsync();
            return await Task.FromResult<bool>(true);
        }

        public async Task<bool> AddPropertiesByCategoryName(AddPropertiesByCategoryNameDto properties)
        {
            properties.PropertyList.ForEach(prop=>prop.CategoryId=_context.Category.Single(ct=>ct.CategoryName==prop.CategoryName).CategoryId);

            _context.Properties.AddRange(properties.PropertyList.Adapt<List<Property>>());
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
        public async Task<List<Property>> GetPropertiesByCategoryName(string categoryName)
        {
            List<Property> result = _context.Properties.Where(p => p.CategoryId == _context.Category.Single(c=>c.CategoryName == categoryName).CategoryId).ToList();
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
