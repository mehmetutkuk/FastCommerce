using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastCommerce.Entities.Entities
{
   public class StockProperties
    {
        [Key]
        public int StockPropertiesId { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        public int PropertyID { get; set; }
        public Property Property { get; set; }
    }
}
