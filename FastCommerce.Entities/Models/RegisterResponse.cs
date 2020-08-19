using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FastCommerce.Entities.Models
{
    public class RegisterResponse
    {
        [DefaultValue(false)]
        public bool SuccessfullyRegistered { get; set; }
    }
}
