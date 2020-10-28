using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.DTOs.Order
{
    public class AddressDto
    {
        public int AddressId { get; set; }
        public string AddressName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Country { get; set; }
        public string AddressLine { get; set; }
        public string TownCity { get; set; }
        public string StateCounty { get; set; }
        public string County { get; set; }
        public string PostalCode { get; set; }
    }
}
