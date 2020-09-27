using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastCommerce.Entities.Entities
{
    public class CategoryProperties
    {
        [Key]
        public int CategoryPropertiesId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
