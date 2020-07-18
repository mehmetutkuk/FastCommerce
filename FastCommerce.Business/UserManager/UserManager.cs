using FastCommerce.DAL;
using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace FastCommerce.Business.UserManager
{
    public class UserManager
    {
        private readonly dbContext _context;
        public UserManager(dbContext context)
        {

            _context = context;
        }
        
        public void AddUser(User user)
        {
             _context.Users.Add(user);
        }

        public void PasiveUser(User user)
        {
            _context.Users.Where(u => u.UserID == user.UserID).FirstOrDefault().Active = false;
        }
            
        public void UpdatePassword (User user)
        {
            _context.Users.Where(w => w.UserID == user.UserID).SingleOrDefault().Password = Cryptography.Encrypt(user.Password);
        }
        

    }
}
