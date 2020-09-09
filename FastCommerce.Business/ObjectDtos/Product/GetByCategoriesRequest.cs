using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.ObjectDtos.Product
{
    public class GetByCategoriesRequest
    {
        public List<Category> Categories { get; set; }
    }
}
