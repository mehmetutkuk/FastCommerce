using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Order
{
    public class GetOrdersByUserRequest
    {
        public List<OrderProduct> orderProducts { get; set; }
    }
}
