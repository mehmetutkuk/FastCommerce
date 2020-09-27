using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Property
{
   public class AddPropertyDto
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public int CategoryId { get; set; }
    }
}
