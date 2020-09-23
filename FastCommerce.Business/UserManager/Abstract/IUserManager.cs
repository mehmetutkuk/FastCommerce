using FastCommerce.Business.DTOs.User;
using FastCommerce.Entities.Entities;
using FastCommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FastCommerce.Business.UserManager.Abstract
{
    public interface IUserManager
    {
        public LoginResponse Login(Login login);
        public RegisterResponse Register(Register register);
        public ActivationResponse ActivateUser(ActivationRequest req);
        public ResetPasswordResponse SendResetPasswordMail(ResetPasswordRequest req);
        public ActivationResponse ResetPassword(ResetPasswordRequest req);
        public void UpdatePassword(User user);
        public void DisableUser(User user);
        public User AddUser(User user);
        public Task<List<User>> GetUsers();
    }
}
