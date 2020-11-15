using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastCommerce.Entities.Entities
{
    public class TrendingProduct
    {
        [Key]
        public int TrendingProductId { get; set; }
        public string CategoryName { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
