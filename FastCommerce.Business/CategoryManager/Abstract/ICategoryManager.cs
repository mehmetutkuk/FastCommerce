using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastCommerce.Business.ProductManager.Abstract
{
  public interface ICategoryManager
    {
        Task<bool> AddCategory(Category category);

        Task<bool> DeleteCategory(Category category);

        Task<bool> UpdateCategory(Category category);

        Task<List<Category>> GetCategories();
    }
}
