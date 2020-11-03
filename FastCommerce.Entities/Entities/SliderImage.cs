using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastCommerce.Entities.Entities
{
    public class SliderImage
    {
        [Key]
        public int SliderImageId { get; set; }
        public string SliderImageName { get; set; }
        public int Order { get; set; }
        public string ImageUrl { get; set; }
        public string SliderHeader { get; set; }
        public string SliderText { get; set; }
        public string SliderNavigationUrl { get; set; }
    }
}
