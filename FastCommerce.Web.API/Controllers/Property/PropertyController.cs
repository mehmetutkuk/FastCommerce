using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FastCommerce.Business.CategoryManager.Abstract;
using FastCommerce.Business.DTOs.Property;
using FastCommerce.Entities.Entities;
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
        /// AddProperty
        /// </summary>
        /// <returns>
        /// <paramref name="Task<HttpResponseMessage>"/>
        /// </returns>

        [HttpPost("AddProperty")]
        public async Task<HttpResponseMessage> AddProperty(AddPropertyDto property)
        {
            Response<Entities.Entities.Property> httpResponse = new Response<Entities.Entities.Property>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _propertyManager.AddProperty(property.Adapt<Entities.Entities.Property>());
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }


        /// <summary>
        /// AddProperties
        /// </summary>
        /// <returns>
        /// <paramref name="Task<HttpResponseMessage>"/>
        /// </returns>

        [HttpPost("AddProperties")]
        public async Task<HttpResponseMessage> AddProperties(AddPropertiesDto properties)
        {
            Response<Entities.Entities.Property> httpResponse = new Response<Entities.Entities.Property>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _propertyManager.AddProperties(properties.PropertyList.Adapt<List<Entities.Entities.Property>>());

            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        [HttpPost("AddPropertyByCategoryName")]
        public async Task<HttpResponseMessage> AddPropertyByCategoryName(AddPropertyByCategoryNameDto property)
        {
            Response<Entities.Entities.Property> httpResponse = new Response<Entities.Entities.Property>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _propertyManager.AddPropertyByCategoryName(property);

            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        [HttpPost("AddPropertiesByCategoryName")]
        public async Task<HttpResponseMessage> AddPropertiesByCategoryName(AddPropertiesByCategoryNameDto properties)
        {
            Response<Entities.Entities.Property> httpResponse = new Response<Entities.Entities.Property>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _propertyManager.AddPropertiesByCategoryName(properties);

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



        [HttpGet("GetGroupedPropertiesByCategoryId/{CategoryId:int}")]
        public async Task<HttpResponseMessage> GetGroupedPropertiesByCategoryId(int CategoryId)
        {
            Response<GroupedPropertyNameDto> httpResponse = new Response<GroupedPropertyNameDto>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _propertyManager.GetGroupedPropertiesByCategoryId(CategoryId);
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }

        [HttpGet("GetPropertiesByCategoryName/{categoryName}")]
        public async Task<HttpResponseMessage> GetPropertiesByCategoryName(string categoryName)
        {
            Response<Entities.Entities.Property> httpResponse = new Response<Entities.Entities.Property>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _propertyManager.GetPropertiesByCategoryName(categoryName);
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        [HttpGet("GetPropertyValuesById/{propertyId}")]
        public async Task<HttpResponseMessage> GetPropertyValuesById(int propertyId)
        {
            Response<PropertyDetail> httpResponse = new Response<PropertyDetail>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _propertyManager.GetPropertyValuesById(propertyId);
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }

        /// <summary>
        /// AddPropertyValues
        /// </summary>
        /// <returns>
        /// <paramref name="Task<HttpResponseMessage>"/>
        /// </returns>

        [HttpPost("AddPropertyValues")]
        public async Task<HttpResponseMessage> AddPropertyValues(List<AddPropertyValuesDto> values)
        {
            Response<Entities.Entities.Property> httpResponse = new Response<Entities.Entities.Property>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _propertyManager.AddPropertyValues(values.Adapt<List<PropertyDetail>>());

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
