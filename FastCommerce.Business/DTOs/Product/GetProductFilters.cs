
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastCommerce.Business.DTOs.Property;
using FastCommerce.Entities.Entities;
using FastCommerce.Entities.Models.Enums;

namespace FastCommerce.Business.DTOs.Product
{
   public class GetProductFilters
    {
        public List<Entities.Entities.Category> productCategoryFilters { get; set; }

        public IEnumerable<List<Entities.Entities.Property>> productPropertyFilters { get; set; }

    }
}
