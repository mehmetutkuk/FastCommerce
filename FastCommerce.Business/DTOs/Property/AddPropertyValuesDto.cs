using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Property
{
    public class AddPropertyValuesDto
    {
        public int PropertyDetailId { get; set; }
        public string PropertyValue { get; set; }
        public int PropertyId { get; set; }
    }

}
