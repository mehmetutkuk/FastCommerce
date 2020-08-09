using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FastCommerce.Entities.Models
{
    public class Login : User
    {
        [DefaultValue(false)]
        public bool LoggedIn { get; set; }
        public string Token { get; set; }
    }
}
