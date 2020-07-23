using FastCommerce.DAL;
using FastCommerce.Entities.Entities;
using FastCommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace FastCommerce.Business.UserManager
{
    public class UserManager : IUserManager
    {
        private readonly dbContext _context;
        public UserManager(dbContext context)
        {
            _context = context;
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            return user;
        }

        public void PasiveUser(User user)
        {
            _context.Users.Where(u => u.UserID == user.UserID).FirstOrDefault().Active = false;
        }

        public void UpdatePassword(User user)
        {
            _context.Users.Where(w => w.UserID == user.UserID).SingleOrDefault().Password = Cryptography.Encrypt(user.Password);
        }

        public Login Login(Login login)
        {
            User fetchedUser = _context.Users.Where(w => (w.Username == login.Username) || (w.Email == login.Email)).SingleOrDefault();
            if (fetchedUser != null)
                login.LoggedIn = (fetchedUser.Password == Cryptography.Encrypt(login.Password));
            return login;
        }

        public Register Register(Register register)
        {
            _context.Users.AddAsync(register);
            register.SuccessfullyRegistered = true;
            return register;
        }

        public Activated Activated(Activated activated)
        {

        }
    }

    public interface IUserManager
    {
        public Login Login(Login login);
        public Register Register(Register register);
    }
}
