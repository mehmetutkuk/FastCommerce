using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FastCommerce.Business.CategoryManager.Abstract;
using FastCommerce.Web.API.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastCommerce.Web.API.Controllers.Property
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyManager _propertyManager;
        public PropertyController(IPropertyManager propertyManager)
        {
            _propertyManager = propertyManager;
        }

        /// <summary>
        /// AddCategory
        /// </summary>
        /// <returns>
        /// <paramref name="Task<HttpResponseMessage>"/>
        /// </returns>

        [HttpPost("AddCategory")]
        public async Task<HttpResponseMessage> AddCategory(Entities.Entities.Property property)
        {
            Response<Entities.Entities.Property> httpResponse = new Response<Entities.Entities.Property>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _propertyManager.AddProperty(property);

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
        public async Task<HttpResponseMessage> DeleteCategory(Entities.Entities.Property property)
        {
            Response<Entities.Entities.Property> httpResponse = new Response<Entities.Entities.Property>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _propertyManager.DeleteProperty(property);

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
        public async Task<HttpResponseMessage> UpdateCategory(Entities.Entities.Property property)
        {
            Response<Entities.Entities.Property> httpResponse = new Response<Entities.Entities.Property>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _propertyManager.UpdateProperty(property);
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

        [HttpGet("Get")]
        public async Task<HttpResponseMessage> Get()
        {
            Response<Entities.Entities.Property> httpResponse = new Response<Entities.Entities.Property>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _propertyManager.GetProperties();
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        [HttpGet("GetCategoryById/{id:int}")]
        public async Task<HttpResponseMessage> GetCategoryById(int id)
        {
            Response<Entities.Entities.Property> httpResponse = new Response<Entities.Entities.Property>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.Data = await _propertyManager.GetPropertiesById(id);
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }

        [HttpGet("GetPropertyByCategoryId/{CategoryId:int}")]
        public async Task<HttpResponseMessage> GetCategories(int CategoryId)
        {
            Response<Entities.Entities.Property> httpResponse = new Response<Entities.Entities.Property>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _propertyManager.GetPropertiesByCategoryId(CategoryId);
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
