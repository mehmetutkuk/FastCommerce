using System;
using System.Collections.Generic;
using System.Text;
using FastCommerce.Entities.Entities;

namespace FastCommerce.Business.DTOs.Order
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public Address Address { get; set; }
        public List<OrderProductDto> Products { get; set; }
        public double TotalPrice { get; set; }
    }
}
