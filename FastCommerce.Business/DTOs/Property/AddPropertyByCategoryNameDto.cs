using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Property
{
    public class AddPropertyByCategoryNameDto
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
