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
        [Key]
        public int UserID { get; set; }
        [Required]
        public string Email { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public string ProfilePhoto { get; set; }
        [Required]
        [DefaultValue("")]
        [NotNull]
        public string Password { get; set; }
        public virtual string PhoneNumber { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        [DefaultValue(false)]
        public bool Active { get; set; }
    }
}
