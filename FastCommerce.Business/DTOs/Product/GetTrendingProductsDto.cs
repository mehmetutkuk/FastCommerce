using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Product
{
    public class GetTrendingProductsDto
    {
        public string CategoryName { get; set; }
        public List<ProductGetDTO> Products { get; set; }
    }
}
