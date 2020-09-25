using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FastCommerce.Business.ProductManager.Abstract;
using FastCommerce.Entities.Entities;
using FastCommerce.Business.DTOs.Categories;
using FastCommerce.Web.API.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace FastCommerce.Web.API.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly ICategoryManager _categoryManager;
        public CategoryController(ICategoryManager categoryManager, IMapper mapper)
        {
            _categoryManager = categoryManager;
            _mapper=mapper ??
                throw new ArgumentNullException(nameof(mapper));;
        }
        /// <summary>
        /// AddCategory
        /// </summary>
        /// <returns>
        /// <paramref name="Task<HttpResponseMessage>"/>
        /// </returns>

        [HttpPost("AddCategory")]
        public IActionResult AddCategory(CategoryForCreationDto category)
        {
        
                var Categories = _mapper.Map<Entities.Entities.Category>(category);
                _categoryManager.AddCategory(Categories);

                var categoryDto = _mapper.Map<CategoryDto>(Categories);


               CreatedAtRoute("GetCategory",
                new { categoryDto.CategoryId },
                categoryDto);
            
        
               
            
            
            return NoContent();
        
        }

            
          
        
/// <summary>
/// DeleteCategory
/// </summary>
/// <returns>
/// <paramref name="Task<HttpResponseMessage>"/>
/// </returns>

[HttpPost("DeleteCategory")]
public  IActionResult  DeleteCategory(int categoryId)
{
   var categories = _categoryManager.GetCategory(categoryId);
   if(categories==null){

       return NotFound();
   }

   _categoryManager.DeleteCategory(categories);
   return Ok(categories);
}

/// <summary>
/// UpdateCategory
/// </summary>
/// <returns>
/// <paramref name="Task<HttpResponseMessage>"/>
/// </returns>

[HttpPost("UpdateCategory")]
public IActionResult UpdateCategory(int categoryId, CategoryForUpdateDto category)
{

      var categories = _categoryManager.GetCategory(categoryId);

         if (categories == null)
            {
                return NotFound();
            }

         _mapper.Map(category, categories);

         _categoryManager.UpdateCategory(categories);
                
            return NoContent();

    
}
/// <summary>
/// GetCategories
/// </summary>
/// <returns>
/// <paramref name="Task<HttpResponseMessage>"/>
/// </returns>

[HttpGet("GetCategories")]
 public ActionResult<CategoryDto> GetCategories(int categoryId){
   
  

         var categories = _categoryManager.GetCategories();

         if (categories == null)
            {
                return NotFound();
            }

         var categoriesdto = _mapper.Map<CategoryDto>(categories);

    

    return Ok(categoriesdto);

}
    }
}
