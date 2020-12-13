using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Product
{
    public class PostProductDTO
    {
        public int[] categoryIds { get; set; } = { };
        public int pageNo { get; set; }
        public int pageSize { get; set; } = 10;
        public int[] propertyDetailIds { get; set; } = { };
    }
}
