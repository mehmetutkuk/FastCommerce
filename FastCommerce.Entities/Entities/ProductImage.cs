using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FastCommerce.Entities.Entities
{
    public class ProductImage
    {
        [Key]
        public int ProductImagesId { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string ImageURL { get; set; }
    }
}
