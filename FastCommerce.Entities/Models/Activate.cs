using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FastCommerce.Entities.Models
{
  
    public class Activate : User
    {
        public enum ActivationTpye
        {
            Phone,
            Email
        }
        public bool SuccelyActivated { get; set; }
        public string activetioncode { get; set; }
        public DateTime startTime { get; set; }

        [DefaultValue(ActivationTpye.Email)]
        public ActivationTpye activationTpye { get; set; }


    }
}
