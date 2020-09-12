using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    }
}
