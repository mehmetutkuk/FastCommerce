using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastCommerce.Business.CategoryManager.Abstract
{
    public interface IPropertyManager
    {

        Task<bool> AddProperty(Property property);

        Task<bool> DeleteProperty(Property property);
        Task<bool> UpdateCategory(Property property);
        Task<List<Property>> GetCategories();

    }
}
