using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastCommerce.Entities.Models
{
    public class ResetPasswordRequest : ActivationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
