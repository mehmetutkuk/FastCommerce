using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastCommerce.Business.AppSettings.Abstract;
using FastCommerce.Business.DTOs.Product;
using FastCommerce.Entities.Entities;
using FastCommerce.Web.API.Models;
using Mapster;

namespace FastCommerce.Web.API.Controllers.AppSettings
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppSettingsController : ControllerBase
    {
        private readonly IAppSettingsManager _appSettingsManager;

        public AppSettingsController(IAppSettingsManager appSettingsManager)
        {
            _appSettingsManager = appSettingsManager;
        }
        /// <summary>
        /// AddProduct
        /// </summary>
        /// <returns>
        /// <paramref name="Task<Response<SliderImage>>"/>
        /// </returns>
        [HttpPost("AddSliderImage")]
        public async Task<Response<SliderImage>> AddSliderImage([FromBody] SliderImage sliderImage)
        {
            var httpResponse = new Response<SliderImage>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _appSettingsManager.AddSliderImage(sliderImage);
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        [HttpPost("UpdateSliderImage")]
        public async Task<Response<SliderImage>> UpdateSliderImage([FromBody]SliderImage sliderImage)
        {
            var httpResponse = new Response<SliderImage>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _appSettingsManager.UpdateSliderImage(sliderImage);
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        [HttpGet("GetSliderImages")]
        public async Task<Response<SliderImage>> GetSliderImages()
        {
            var httpResponse = new Response<SliderImage>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _appSettingsManager.GetSliderImages();
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        [HttpGet("DeleteSliderImage")]
        public async Task<Response<SliderImage>> DeleteSliderImage([FromBody] SliderImage sliderImage)
        {
            var httpResponse = new Response<SliderImage>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _appSettingsManager.DeleteSliderImage(sliderImage);
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
