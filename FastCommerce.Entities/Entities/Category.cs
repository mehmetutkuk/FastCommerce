using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastCommerce.Entities.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int ProductCategoriesId { get; set; }
        public ICollection<ProductCategories> ProductCategories { get; set; }
        public ICollection <Property> Properties { get; set; }

    }
}
