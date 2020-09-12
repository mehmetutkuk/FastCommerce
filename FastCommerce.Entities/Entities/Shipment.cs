using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Entities.Entities
{
   public class Shipment
    {
        public int ShipmentId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
