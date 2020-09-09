using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using FastCommerce.Entities.Models;

namespace FastCommerce.Entities.Entities
{
    public class UserActivation
    {
        public int Id { get; set; }
        public User User { get; set; }
        public bool isActivated { get; set; }
        public string ActivationCode { get; set; }
        public DateTime StartTime { get; set; }

        [DefaultValue(ActivationChannelType.Email)]
        public ActivationChannelType ActivationChannelType { get; set; }
        public ActivationType ActivationType { get; set; }

    }
}
