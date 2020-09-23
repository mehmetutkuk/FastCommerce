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

        [HttpPost("AddProperty")]
        public async Task<HttpResponseMessage> AddProperty(Entities.Entities.Property property)
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

        [HttpPost("DeleteProperty")]
        public async Task<HttpResponseMessage> DeleteProperty(Entities.Entities.Property property)
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

        [HttpPost("UpdateProperty")]
        public async Task<HttpResponseMessage> UpdateProperty(Entities.Entities.Property property)
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
        [HttpGet("Get/{id:int}")]
        public async Task<HttpResponseMessage> Get(int id)
        {
            Response<Entities.Entities.Property> httpResponse = new Response<Entities.Entities.Property>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.Data = await _propertyManager.GetPropertyById(id);
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }

        [HttpGet("GetPropertiesByCategoryId/{CategoryId:int}")]
        public async Task<HttpResponseMessage> GetPropertiesByCategoryId(int CategoryId)
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
