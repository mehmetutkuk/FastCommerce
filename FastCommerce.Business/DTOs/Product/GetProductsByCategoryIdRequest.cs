using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Product
{
    public class GetProductsByCategoryIdRequest
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
