using FastCommerce.Business.Core;
using FastCommerce.Business.ElasticSearch.Conrete;
using System;
using System.Collections.Generic;

namespace FastCommerce.Business.ObjectDtos.Product
{
    public class ProductElasticIndexDto : ElasticEntity<int>, IDto
    {
        public ProductElasticIndexDto()
        {
         
        }
         
        public virtual string ProductName { get; set; }
        public virtual string ProductId { get; set; }
        public virtual string CategoryName { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual int UserId { get; set; }
        public virtual DateTime CreatedDate { get; set; }
    }
}