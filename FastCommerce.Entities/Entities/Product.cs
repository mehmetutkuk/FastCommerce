using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastCommerce.Entities.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [ForeignKey("ProductCategoriesId")]
        public int ProductCategoriesId { get; set; }
        public ICollection<ProductCategories> ProductCategories { get; set; }
        public DateTime LastModified { get; set; }
        public double Rating { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
        public int ViewCount { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
