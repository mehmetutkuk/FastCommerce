using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FastCommerce.Business.DTOs.User
{
    public class LoginResponse
    {
        [DefaultValue(false)]
        public bool LoggedIn { get; set; }
        public string Token { get; set; }
    }
}
