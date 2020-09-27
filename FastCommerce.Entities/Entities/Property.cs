using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FastCommerce.Entities.Entities
{
    public class Property
    {
        [Key]
        public int PropertyID { get; set; }
        [Required]
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public ICollection<StockProperties> StockProperties { get; set; }
    }
}
