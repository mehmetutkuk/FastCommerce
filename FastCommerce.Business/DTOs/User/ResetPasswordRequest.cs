using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastCommerce.Business.DTOs.User
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ActivationCode { get; set; }
    }
}
