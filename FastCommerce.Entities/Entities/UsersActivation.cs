using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FastCommerce.Entities.Entities
{
    public class UsersActivation
    {
        public int Id { get; set; }
        public  User user { get; set; }
        public bool SuccelyActivated { get; set; }
        public string activetioncode { get; set; }
        public DateTime startTime { get; set; }

        [DefaultValue(ActivationTpye.Email)]
        public ActivationTpye activationTpye { get; set; }

        public enum ActivationTpye
        {
            Phone,
            Email
        }
    }
}
