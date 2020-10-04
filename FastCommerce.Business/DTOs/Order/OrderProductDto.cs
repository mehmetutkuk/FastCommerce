using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Order
{
    public class OrderProductDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
