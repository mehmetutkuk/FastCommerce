using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FastCommerce.Business.DTOs.Order;
using FastCommerce.Business.OrderManager;
using FastCommerce.Business.OrderManager.Abstract;
using FastCommerce.Entities.Entities;
using FastCommerce.Entities.Models;
using FastCommerce.Web.API.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastCommerce.Web.API.Controllers.Orders
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
        public async Task<Response<Order>> AddOrder(Order order)
        {
            Response<Order> _response = new Response<Order>();
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
        public async Task<Response<Order>> DeleteOrder(Order OrderId)
        {
            Response<Order> _response = new Response<Order>();
            try
            {
                _response.RequestState = true;
                _response.ErrorState = !await _orderManager.DeleteOrder(OrderId);
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
        public async Task<Response<UpdateCharacterDto>> UpdateOrder(UpdateCharacterDto OrderId)
        {
            Response<UpdateCharacterDto> _response = new Response<UpdateCharacterDto>();
            try
            {
                _response.RequestState = true;
                _response.ErrorState = !await _orderManager.UpdateOrder(OrderId.OrderId);
            }
            catch (Exception ex)
            {
                _response.ErrorState = true;
                _response.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return _response;
        }

        public Task GetOrdersByUser(GetOrdersByUserRequest getOrdersByUserRequest)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GetOrdersByUser Method
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>

        [HttpGet("GetOrdersByUser")]
        public Response<OrderGetDTO> GetOrdersByUser(int UserId)
        {
            Response<OrderGetDTO> _response = new Response<OrderGetDTO>();
            try
            {
                _response.RequestState = true;
                _response.Data = _orderManager.GetOrdersByUser(UserId);
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
        public async Task<Response<OrderGetDTO>> GetOrders()
        {
            Response<OrderGetDTO> _response = new Response<OrderGetDTO>();
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
