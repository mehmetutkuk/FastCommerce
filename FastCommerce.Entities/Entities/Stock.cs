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
        public virtual Product Product { get; set; }
        public virtual List<Property> Properties { get; set; }
        public int Quantity { get; set; }
    }
}
