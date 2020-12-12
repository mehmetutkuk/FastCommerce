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
using Microsoft.Extensions.Logging;

namespace FastCommerce.Web.API.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductManager ProductManager, ILogger<ProductController> logger)
        {
            _productManager = ProductManager;
            _logger = logger;
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
            _logger.LogDebug("GetProduct init with");
            Response<ProductGetDTO> httpResponse = new Response<ProductGetDTO>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _productManager.Get();
                httpResponse.EntityCount = httpResponse.DataList.Count();
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetProduct Error", ex);
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            _logger.LogDebug("GetProduct end with", httpResponse);
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


        [HttpGet("GetProductByPageNumber/{pageNo:int}")]
        public async Task<Response<ProductGetDTO>> GetProductByPageNumber(int pageNo)
        {
            _logger.LogDebug("GetProductByPageNumber init with",pageNo);
            Response<ProductGetDTO> httpResponse = new Response<ProductGetDTO>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _productManager.GetProductByPageNumber(pageNo);
                httpResponse.EntityCount = httpResponse.DataList.Count();
            }
            catch (Exception ex)
            {
              _logger.LogError("GetProductByPageNumber Error", ex);
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            _logger.LogDebug("GetProductByPageNumber end with", httpResponse);
            return await Task.FromResult(httpResponse);
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
            _logger.LogDebug("GetById init with", id);
            Response<ProductGetDTO> httpResponse = new Response<ProductGetDTO>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.Data =  await _productManager.GetProductById(id);
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetById Error", ex);
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            _logger.LogDebug("GetById end with", httpResponse);
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
            _logger.LogDebug("SearchProduct init with", search);
            Response<ProductElasticIndexDto> httpResponse = new Response<ProductElasticIndexDto>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _productManager.SuggestProductSearchAsync(search);
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                _logger.LogError("SearchProduct Error", ex);

                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            _logger.LogDebug("SearchProduct end with", httpResponse);
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
            _logger.LogDebug("AddProduct init with", product);
            Response<Product> httpResponse = new Response<Product>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState = !await _productManager.AddProduct(product);
            }
            catch (Exception ex)
            {
                _logger.LogError("AddProduct Error", ex);
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            _logger.LogDebug("AddProduct end with", httpResponse);
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
            _logger.LogDebug("GetProductsByCategoryId init with", id);
            Response<ProductGetDTO> httpResponse = new Response<ProductGetDTO>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _productManager.GetProductsByCategoryId(id);
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetProductsByCategoryId Error", ex);
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            _logger.LogDebug("GetProductsByCategoryId end with", httpResponse);
            return httpResponse;
        }
        
        [HttpGet("GetProductsByCategoryName/{name}")]
        public async Task<Response<ProductGetDTO>> GetProductsByCategoryName(string name)
        {
            _logger.LogDebug("GetProductsByCategoryName init with", name);
            Response<ProductGetDTO> httpResponse = new Response<ProductGetDTO>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _productManager.GetProductsByCategoryName(name);
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetProductsByCategoryName Error", ex);

                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            _logger.LogDebug("GetProductsByCategoryName end with", httpResponse);

            return httpResponse;
        }
        
        [HttpGet("GetProductFilters")]
        public async Task<Response<GetProductFilters>> GetProductFilters()
        {
            _logger.LogDebug("GetProductsByCategoryName init with");
            Response<GetProductFilters> httpResponse = new Response<GetProductFilters>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.Data = await _productManager.GetProductFilters();
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetProductsByCategoryName Error", ex);
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            _logger.LogDebug("GetProductsByCategoryName end with", httpResponse);
            return httpResponse;
        }

        [HttpGet("GetMinMaxPrice")]
        public async Task<Response<GetMinMaxPriceDto>> GetMinMaxPrice()
        {
            _logger.LogDebug("GetMinMaxPrice init with");
            Response<GetMinMaxPriceDto> httpResponse = new Response<GetMinMaxPriceDto>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.Data = await _productManager.GetMinMaxPrice();
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetMinMaxPrice Error", ex);
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            _logger.LogDebug("GetMinMaxPrice end with", httpResponse);
            return httpResponse;
        }
    }
}
