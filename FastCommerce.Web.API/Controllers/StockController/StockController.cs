using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FastCommerce.Business.DTOs.Stock;
using FastCommerce.Business.StockManager.Concrete;
using FastCommerce.Entities.Entities;
using FastCommerce.Web.API.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastCommerce.Web.API.Controllers.StockController
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        public readonly IStockManager _stockManager;
        public StockController(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }



        /// <summary>
        /// GetStocks
        /// </summary>
        /// <returns>
        /// <paramref name="Task<HttpResponseMessage>"/>
        /// </returns>

        [HttpGet("GetStocks")]
        public async Task<HttpResponseMessage> GetStocks()
        {
            Response<GetStocksDto> httpResponse = new Response<GetStocksDto>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.DataList = await _stockManager.GetStocks();
                httpResponse.ErrorState = false;
            }
            catch (Exception ex)
            {
                httpResponse.ErrorState = true;
                httpResponse.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return httpResponse;
        }

        [HttpPost("UpdateQuantity")]
        public async Task<HttpResponseMessage> UpdateQuantity(UpdateQuantityDto updateQuantityDto)
        {
            Response<GetStocksDto> httpResponse = new Response<GetStocksDto>();
            try
            {
                httpResponse.RequestState = true;
                httpResponse.ErrorState  = !await _stockManager.UpdateQuantityByStockId(updateQuantityDto.StockId, updateQuantityDto.Quantity);
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
