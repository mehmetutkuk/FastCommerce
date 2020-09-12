using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FastCommerce.Business.ProductManager.Abstract;
using FastCommerce.Entities.Entities;
using FastCommerce.Web.API.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastCommerce.Web.API.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }
        /// <summary>
        /// AddCategory
        /// </summary>
        /// <returns>
        /// <paramref name="Task<HttpResponseMessage>"/>
        /// </returns>
        
        [HttpPost("AddCategory")]
        public async Task<HttpResponseMessage> AddCategory(Entities.Entities.Category category)
        {
            Response<Entities.Entities.Category> httpResponse = new  Response<Entities.Entities.Category>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState  = !await _categoryManager.AddCategory(category);
                
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        /// <summary>
        /// DeleteCategory
        /// </summary>
        /// <returns>
        /// <paramref name="Task<HttpResponseMessage>"/>
        /// </returns>
        
        [HttpPost("DeleteCategory")]
        public async Task<HttpResponseMessage> DeleteCategory(Entities.Entities.Category category)
        {
            Response<Entities.Entities.Category> httpResponse = new Response<Entities.Entities.Category>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _categoryManager.DeleteCategory(category);
                
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }

        /// <summary>
        /// UpdateCategory
        /// </summary>
        /// <returns>
        /// <paramref name="Task<HttpResponseMessage>"/>
        /// </returns>
        
        [HttpPost("UpdateCategory")]
        public async Task<HttpResponseMessage> UpdateCategory(Entities.Entities.Category category)
        {
            Response<Entities.Entities.Category> httpResponse = new Response<Entities.Entities.Category>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _categoryManager.UpdateCategory(category);
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
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
            return httpResponse;
        }
    }
}
