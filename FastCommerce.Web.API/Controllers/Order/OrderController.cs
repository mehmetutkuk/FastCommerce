using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using FastCommerce.Business.DTOs.Order;
using FastCommerce.Business.OrderManager.Abstract;
using FastCommerce.Entities.Entities;
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

        [HttpPost("CreateOrder")]
        public async Task<HttpResponseMessage> CreateOrder(CreateOrderDto order)
        {
            Response<Entities.Entities.Order> _response = new Response<Entities.Entities.Order>();
            try
            {
                order.UserId = int.Parse(HttpContext.User.FindFirstValue("id"));
                _response.RequestState = true;
                _response.ErrorState = !await _orderManager.CreateOrder(order);
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
        [HttpGet("GetAddresses")]
        public async Task<HttpResponseMessage> GetAddresses()
        {
            Response<AddressDto> _response = new Response<AddressDto>();
            try
            {
                int userId = int.Parse(HttpContext.User.FindFirstValue("id"));
                _response.RequestState = true;
                _response.DataList = await _orderManager.GetAddressesByUser(userId);
                _response.ErrorState = false;
            }
            catch (ArgumentNullException ex)
            {
                _response.ErrorState = true;
                _response.ErrorList.Add(new ApiException(){Message = "Unauthorized Access",Detail="Token is not provided so the userId required for getting the addresses."});
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
