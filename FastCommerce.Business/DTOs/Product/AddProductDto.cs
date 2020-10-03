using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Product
{
    public class AddProductDto
    {
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public List<AddProductProperties> Properties { get; set; }

        public List<AddProductImages> Images { get; set; }

    }

    public class AddProductImages
    {
        public string Img { get; set; }
    }

    public class AddProductProperties
    {
        public int CategoryId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
    }



}
