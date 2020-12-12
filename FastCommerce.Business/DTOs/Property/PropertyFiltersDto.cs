using FastCommerce.Entities.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Property
{
   public class PropertyFiltersDto
    {
        public int PropertyID { get; set; }
        public string PropertyName { get; set; }
        public PropertyType PropertyType { get; set; }
        public string PropertyValue { get; set; }
    }
}
