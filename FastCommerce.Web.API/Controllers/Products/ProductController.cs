using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FastCommerce.Business.DTOs.Product;
using FastCommerce.Business.ProductManager;
using FastCommerce.Business.ProductManager.Abstract;
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
        private readonly IProductManager _productManager;

        public ProductController(IProductManager ProductManager)
        {
            _productManager = ProductManager;
        }


        /// <summary>
        /// Get
        /// </summary>
        /// <returns>
        /// <paramref name="Task<Response<Product>>"/>
        /// </returns>
        [HttpGet("Get")]
        public async Task<Response<ProductGetDTO>> Get()
        {
            Response<ProductGetDTO> httpResponse = new Response<ProductGetDTO>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _productManager.Get();
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }

        [HttpGet("GetTrendingProducts")]
        public async Task<Response<GetTrendingProductsDto>> GetTrendingProducts()
        {
            var httpResponse = new Response<GetTrendingProductsDto>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _productManager.GetTrendingProducts();
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        [HttpGet("GetTrendingProductEntities")]
        public async Task<Response<TrendingProduct>> GetTrendingProductEntities()
        {
            var httpResponse = new Response<TrendingProduct>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _productManager.GetTrendingProductEntities();
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        [HttpPost("AddTrendingProduct")]
        public async Task<Response<TrendingProduct>> AddTrendingProduct(TrendingProduct trendingProduct)
        {
            var httpResponse = new Response<TrendingProduct>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _productManager.AddTrendingProduct(trendingProduct);
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        [HttpPost("RemoveTrendingProduct")]
        public async Task<Response<TrendingProduct>> RemoveTrendingProduct(RemoveTrendingProductDto trendingProduct)
        {
            var httpResponse = new Response<TrendingProduct>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _productManager.RemoveTrendingProduct(trendingProduct);
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        [HttpPost("UpdateTrendingProduct")]
        public async Task<Response<TrendingProduct>> UpdateTrendingProduct(TrendingProduct trendingProduct)
        {
            var httpResponse = new Response<TrendingProduct>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _productManager.UpdateTrendingProduct(trendingProduct);
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        /// <summary>
        /// GetProductById
        /// </summary>
        /// <returns>
        /// <paramref name="Task<Response<Product>>"/>
        /// </returns>
        [HttpGet("Get/{id:int}")]
        public async Task<Response<ProductGetDTO>> Get(int id)
        {
            Response<ProductGetDTO> httpResponse = new Response<ProductGetDTO>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.Data =  await _productManager.GetProductById(id);
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
        /// SearchProduct
        /// </summary>
        /// <returns>
        /// <paramref name="Task<Response<Product>>"/>
        /// </returns>
        [HttpGet("SearchProduct")]
        public async Task<Response<ProductElasticIndexDto>> SearchProduct(string search)
        {
            Response<ProductElasticIndexDto> httpResponse = new Response<ProductElasticIndexDto>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _productManager.SuggestProductSearchAsync(search);
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
        /// AddProduct
        /// </summary>
        /// <returns>
        /// <paramref name="Task<Response<Product>>"/>
        /// </returns>
        [HttpPost("AddProduct")]
        public async Task<Response<Product>> AddProduct(AddProductDto product)
        {
            Response<Product> httpResponse = new Response<Product>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _productManager.AddProduct(product);
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        /// <summary>
        /// GetByPlace
        /// </summary>
        /// <returns>
        /// <paramref name="Task<Response<Product>>"/>
        /// </returns>
        [HttpGet("GetProductsByCategoryId/{id:int}")]
        public async Task<Response<ProductGetDTO>> GetProductsByCategoryId(int id)
        {
            Response<ProductGetDTO> httpResponse = new Response<ProductGetDTO>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _productManager.GetProductsByCategoryId(id);
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }
        [HttpGet("GetProductsByCategoryName/{name}")]
        public async Task<Response<ProductGetDTO>> GetProductsByCategoryName(string name)
        {
            Response<ProductGetDTO> httpResponse = new Response<ProductGetDTO>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _productManager.GetProductsByCategoryName(name);
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
