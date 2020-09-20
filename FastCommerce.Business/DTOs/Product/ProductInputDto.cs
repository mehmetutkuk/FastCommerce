
using FastCommerce.Business.Core;
using FastCommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Product
{
    public class ProductInputDto : IDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; } 
        public int ReadCount { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public List<int> PostTagIds { get; set; }
    }
}
