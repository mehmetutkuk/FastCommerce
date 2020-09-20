using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Entities.Entities
{
    public class Address
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string BuildingNo { get; set; }
        public string DoorNo { get; set; }
        public string AddressLine { get; set; }
    }
}
