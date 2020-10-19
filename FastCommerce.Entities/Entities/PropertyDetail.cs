using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastCommerce.Entities.Entities
{
    public class PropertyDetail
    {
        [Key]
        public int PropertyDetailId { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
        public string PropertyValue { get; set; }
        public virtual StockPropertyCombination StockPropertyCombination { get; set; }
    }
}
