using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastCommerce.Entities.Entities
{
    public class Stock
    {
        [Key]
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public ICollection<StockProperties> StockProperties { get; set; } 
        public int Quantity { get; set; }
    }
}
