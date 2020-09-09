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

    }
}