using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Stock
{
    public class UpdateQuantityDto
    {
        public int StockId { get; set; }
        public int Quantity { get; set; }
    }
}
