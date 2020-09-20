using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FastCommerce.Business.DTOs.User
{
    public class ResetPasswordResponse
    {
        [DefaultValue(false)]
        public bool isResetMailSent { get; set; }
    }
}
