using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FastCommerce.Business.DTOs.Category;
using FastCommerce.Business.ProductManager.Abstract;
using FastCommerce.Entities.Entities;
using FastCommerce.Web.API.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FastCommerce.Web.API.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ICategoryManager categoryManager, ILogger<CategoryController> logger)
        {
            _categoryManager = categoryManager;
            _logger = logger;
        }
        /// <summary>
        /// AddCategory
        /// </summary>
        /// <returns>
        /// <paramref name="Task<HttpResponseMessage>"/>
        /// </returns>
        
        [HttpPost("AddCategory")]
        public async Task<HttpResponseMessage> AddCategory(AddCategoryDto req)
        {
            _logger.LogDebug("AddCategory init with",req);
            Response<Entities.Entities.Category> httpResponse = new  Response<Entities.Entities.Category>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState  = !await _categoryManager.AddCategory(req);
            }
            catch (Exception ex)
            {
                _logger.LogError("AddCategory Error", ex);
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            _logger.LogDebug("AddCategory end with", httpResponse);
            return httpResponse;
        }
        /// <summary>
        /// DeleteCategory
        /// </summary>
        /// <returns>
        /// <paramref name="Task<HttpResponseMessage>"/>
        /// </returns>
        
        [HttpPost("DeleteCategory")]
        public async Task<HttpResponseMessage> DeleteCategory(DeleteCategoryDto category)
        {
            _logger.LogDebug("DeleteCategory init with", category);
            Response<Entities.Entities.Category> httpResponse = new Response<Entities.Entities.Category>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _categoryManager.DeleteCategory(category.Adapt<Entities.Entities.Category>());
                
            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteCategory Error", ex);
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            _logger.LogDebug("DeleteCategory end with", httpResponse);
            return httpResponse;
        }

        /// <summary>
        /// UpdateCategory
        /// </summary>
        /// <returns>
        /// <paramref name="Task<HttpResponseMessage>"/>
        /// </returns>
        
        [HttpPost("UpdateCategory")]
        public async Task<HttpResponseMessage> UpdateCategory(UpdateCategoryDto category)
        {
            _logger.LogDebug("UpdateCategory init with", category);

            Response<Entities.Entities.Category> httpResponse = new Response<Entities.Entities.Category>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _categoryManager.UpdateCategory(category.Adapt<Entities.Entities.Category>());
            }
            catch (Exception ex)
            {
                _logger.LogError("UpdateCategory Error", ex);
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            _logger.LogDebug("UpdateCategory end with", httpResponse);
            return httpResponse;
        }
        /// <summary>
        /// GetCategories
        /// </summary>
        /// <returns>
        /// <paramref name="Task<HttpResponseMessage>"/>
        /// </returns>
        
        [HttpGet("GetCategories")]
        public async Task<HttpResponseMessage> GetCategories()
        {
            _logger.LogDebug("GetCategories init");
            Response<Entities.Entities.Category> httpResponse = new Response<Entities.Entities.Category>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _categoryManager.GetCategories();
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            _logger.LogDebug("GetCategories end with", httpResponse);
            return httpResponse;
        }
    }
}
