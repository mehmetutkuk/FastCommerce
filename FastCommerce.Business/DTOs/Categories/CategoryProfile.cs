using AutoMapper;
using FastCommerce.Entities.Entities;
namespace FastCommerce.Business.DTOs.Categories
{
    public class CategoryProfile: Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
        
        }

        
    }
}