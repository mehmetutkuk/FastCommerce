using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FastCommerce.Business.OrderManager.Abstract;
using FastCommerce.Web.API.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastCommerce.Web.API.Controllers.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        /// <summary>
        /// AddOrder Method
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>

        [HttpPost("AddOrder")]
        public async Task<HttpResponseMessage> AddOrder(Entities.Entities.Order order)
        {
            Response<Entities.Entities.Order> _response = new Response<Entities.Entities.Order>();
            try
            {
                _response.RequestState = true;
                _response.ErrorState = !await _orderManager.AddOrder(order);
            }
            catch (Exception ex)
            {
                _response.ErrorState = true;
                _response.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return _response;
        }
        /// <summary>
        /// DeleteOrder Method
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>

        [HttpPost("DeleteOrder")]
        public async Task<HttpResponseMessage> DeleteOrder(Entities.Entities.Order order)
        {
            Response<Entities.Entities.Order> _response = new Response<Entities.Entities.Order>();
            try
            {
                _response.RequestState = true;
                _response.ErrorState = !await _orderManager.DeleteOrder(order);
            }
            catch (Exception ex)
            {
                _response.ErrorState = true;
                _response.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return _response;
        }

        /// <summary>
        /// UpdateOrder Method
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>

        [HttpPost("UpdateOrder")]
        public async Task<HttpResponseMessage> UpdateOrder(Entities.Entities.Order order)
        {
            Response<Entities.Entities.Order> _response = new Response<Entities.Entities.Order>();
            try
            {
                _response.RequestState = true;
                _response.ErrorState = !await _orderManager.UpdateOrder(order);
            }
            catch (Exception ex)
            {
                _response.ErrorState = true;
                _response.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return _response;
        }
        /// <summary>
        /// GetOrdersByUser Method
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>

        [HttpGet("GetOrdersByUser")]
        public async Task<HttpResponseMessage> GetOrdersByUser(int UserId)
        {
            Response<Entities.Entities.Order> _response = new Response<Entities.Entities.Order>();
            try
            {
                _response.RequestState = true;
                _response.DataList = await _orderManager.GetOrdersByUser(UserId);
                _response.ErrorState = false;
            }
            catch (Exception ex)
            {
                _response.ErrorState = true;
                _response.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return _response;
        }
        /// <summary>
        /// GetOrders Method
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>

        [HttpGet("GetOrders")]
        public async Task<HttpResponseMessage> GetOrders()
        {
            Response<Entities.Entities.Order> _response = new Response<Entities.Entities.Order>();
            try
            {
                _response.RequestState = true;
                _response.DataList = await _orderManager.GetOrders();
                _response.ErrorState = false;
            }
            catch (Exception ex)
            {
                _response.ErrorState = true;
                _response.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return _response;
        }
    }
}
