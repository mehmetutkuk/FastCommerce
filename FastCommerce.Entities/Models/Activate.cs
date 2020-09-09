using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FastCommerce.Entities.Models
{
  
    public class Activate : User
    {

        public bool isActivated { get; set; }
        public string ActivationCode { get; set; }
        public DateTime StartTime { get; set; }
        [DefaultValue(ActivationChannelType.Email)]
        public ActivationChannelType ActivationType { get; set; }
    }
}