using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FastCommerce.Entities.Entities
{
   public class StockPropertyCombination
    {
        [Key]
        public int StockPropertyCombinationId { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }

        [ForeignKey("PropertyDetailId")]
        public int PropertyDetailId { get; set; }
        public PropertyDetail PropertyDetail { get; set; }
    }
}
