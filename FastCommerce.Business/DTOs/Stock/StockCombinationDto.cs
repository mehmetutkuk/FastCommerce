using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Stock
{
    public class StockCombinationDto
    {
        public int Key { get; set; }
        public List<PropertyDetail> Value { get; set; }
    }

}
