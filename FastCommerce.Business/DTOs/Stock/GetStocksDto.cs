using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Stock
{
    public class GetStocksDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int StockPropertiesId { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public List<GetStocksDtoProperty> properties { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }

    public class GetStocksDtoProperty {

        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
    }
}
