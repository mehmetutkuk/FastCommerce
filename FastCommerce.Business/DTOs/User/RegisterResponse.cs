using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FastCommerce.Business.DTOs.User
{
    public class RegisterResponse
    {
        [DefaultValue(false)]
        public bool SuccessfullyRegistered { get; set; }
    }
}
