using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastCommerce.Entities.Models
{
    public class Register : User
    {
        [DefaultValue(false)]
        public bool SuccessfullyRegistered { get; set; }

        public bool ValidForRegister()
        {
            bool valid = true;
            if (Email == null || PhoneNumber == null)
                valid = false;
            if (Password == null)
                valid = false;
            if (Name == null && Surname == null)
                valid = false;
            return valid;
        }
        [Required]
        public override string Name { get; set; }
        [Required]
        public override string Surname { get; set; }
        [Required]
        public virtual string PhoneNumber { get; set; }

    }
}