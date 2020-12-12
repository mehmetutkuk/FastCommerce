using FastCommerce.Entities.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DefaultValue(PropertyType.String)]
        public PropertyType PropertyType { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<PropertyDetail> PropertyDetails { get; set; }

    }
}
 