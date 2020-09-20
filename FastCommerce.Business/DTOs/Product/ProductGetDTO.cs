using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Product
{
    public class ProductGetDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }        
        public double Rating { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
