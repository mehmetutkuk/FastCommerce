using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace FastCommerce.Entities.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        
        public string ProfilePhoto { get; set; }
        [Required]
        [DefaultValue("")]
        [NotNull]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        [DefaultValue(false)]
        public bool Active { get; set; }

    }
}
