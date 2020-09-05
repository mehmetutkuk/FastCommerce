using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FastCommerce.Business.ProductManager;
using FastCommerce.Entities.Entities;
using FastCommerce.Entities.Models;
using FastCommerce.Web.API.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastCommerce.Web.API.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IProductManager _productManager;
        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }
        [ActionName("Save"), Route("Save")]
        [HttpPost]
        public async Task<HttpResponseMessage> SaveAsync(Product product) {
            Response<Product> httpResponse = new Response<Product>();
            try
            {
                httpResponse.RequestState= true;
           //     await ProductManager.SaveProduct(product);
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        [HttpGet("Get")]
        public async Task<HttpResponseMessage> Get()
        {
            Response<Product> httpResponse = new Response<Product>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = _productManager.Get();
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        [HttpGet("GetByPlace")]
        public async Task<HttpResponseMessage> GetByCategories([FromBody]GetByCategoriesRequest req)
        {
            Response<Product> httpResponse = new Response<Product>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = _productManager.GetByCategories(req);
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        [HttpPost("SetPlaces")]
        public async Task<HttpResponseMessage> SetPlaces([FromBody]SetPlacesRequest req)
        {
            Response<Product> httpResponse = new Response<Product>();
            try
            {
                httpResponse.RequestState = true;
                _productManager.SetPlaces(req);
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
