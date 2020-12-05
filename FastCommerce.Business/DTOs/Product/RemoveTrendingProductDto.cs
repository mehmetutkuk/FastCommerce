using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Product
{
    public class RemoveTrendingProductDto
    {
        public int TrendingProductId { get; set; }
        public string CategoryName { get; set; }
        public int DisplayOrder { get; set; }
        public int ProductId { get; set; }
    }
}
