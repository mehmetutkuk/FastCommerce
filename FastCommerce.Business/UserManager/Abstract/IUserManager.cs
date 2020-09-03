using FastCommerce.Entities.Entities;
using FastCommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastCommerce.Business.UserManager.Abstract
{
    public interface IUserManager
    {
        public Login Login(Login login);
        public Register Register(Register register);
        public UserActivation ActivateUser(string code);
    }
}
