using FastCommerce.Business.Core;
using FastCommerce.Business.ElasticSearch.Concrete;
using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Order
{
    public class OrderElasticIndexDto :ElasticEntity<int>,IDto
    {
        public OrderElasticIndexDto() {}
        public virtual int OrderId { get; set; }
        public virtual int UserId { get; set; }
        public virtual int Stage { get; set; }
        public virtual Shipment Shipment { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
