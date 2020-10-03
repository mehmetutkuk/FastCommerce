using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FastCommerce.Business.DTOs.Property;
using System.Linq;

namespace FastCommerce.Business.CategoryManager.Abstract
{
    public interface IPropertyManager
    {

        Task<bool> AddProperty(Property property);
        Task<bool> AddProperties(List<Property> properties);
        Task<bool> AddPropertiesByCategoryName(AddPropertiesByCategoryNameDto properties);
        Task<bool> AddPropertyByCategoryName(AddPropertyByCategoryNameDto property);
        Task<bool> DeleteProperty(Property property);
        Task<bool> UpdateProperty(Property property);
        Task<List<Property>> GetProperties();
        
        Task<List<Property>> GetPropertiesByCategoryId(int categoryId);
        Task<List<GroupedPropertyNameDto>> GetGroupedPropertiesByCategoryId(int CategoryId);
        Task<List<Property>> GetPropertiesByCategoryName(string categoryName);

        Task<Property> GetPropertyById(int Id);
    }
}
