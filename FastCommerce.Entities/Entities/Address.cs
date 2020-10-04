using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastCommerce.Entities.Entities
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Country { get; set; }
        public string AddressLine { get; set; }
        public string TownCity { get; set; }
        public string StateCounty { get; set; }
        public string PostalCode { get; set; }
    }
}
