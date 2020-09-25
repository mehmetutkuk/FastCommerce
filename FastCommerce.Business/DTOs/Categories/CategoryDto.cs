using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FastCommerce.Entities.Entities;

namespace FastCommerce.Business.DTOs.Categories
{
    public class CategoryDto
    {
         
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}