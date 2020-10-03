using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Order
{
    public class UpdateCharacterDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int Stage { get; set; }
        public Shipment Shipment { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
