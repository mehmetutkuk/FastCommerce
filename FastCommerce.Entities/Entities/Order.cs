using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Entities.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Stage { get; set; }
        public Shipment Shipment { get; set; }
        public int OrderProductsId { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
