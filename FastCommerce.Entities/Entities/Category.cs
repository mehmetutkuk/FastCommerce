using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastCommerce.Entities.Entities
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string Categoryname { get; set; }
        [Required]
        public virtual List<Property> Properties { get; set; }

    }
}
