using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            if (Username == null || (Email == null || PhoneNumber == null))
                valid = false;
            if (Password == null)
                valid = false;
            if (Name == null && Surname == null)
                valid = false;
            return valid;
        }

    }
}