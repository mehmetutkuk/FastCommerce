using FastCommerce.Business.DTOs.User;
using FastCommerce.Business.UserManager.Abstract;
using FastCommerce.DAL;
using FastCommerce.Entities.Entities;
using FastCommerce.Entities.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility.Cryptography;
using Utility.MailServices;
using Utility.Models;

namespace FastCommerce.Business.UserManager.Concrete
{
    public class UserManager : IUserManager
    {
        private readonly TokenModel _tokenManagement;
        private readonly dbContext _context;
        private IEmailService _mailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserManager(dbContext context, IEmailService mailService, IOptions<TokenModel> tokenManagement, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mailService = mailService;
            _httpContextAccessor = httpContextAccessor;
            _tokenManagement = tokenManagement.Value;
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            return user;
        }



        public void DisableUser(User user)
        {
            _context.Users.Where(u => u.UserID == user.UserID).FirstOrDefault().Active = false;
        }

        public void UpdatePassword(User user)
        {
            _context.Users.Where(w => w.UserID == user.UserID).SingleOrDefault().Password = Cryptography.Encrypt(user.Password);
        }

        public LoginResponse Login(Login login)
        {
            User fetchedUser = _context.Users.Where(w => w.Email == login.Email).SingleOrDefault();
            if (fetchedUser != null)
            {
                login.LoggedIn = (fetchedUser.Password == Cryptography.Encrypt(login.Password));
                if (login.LoggedIn)
                {
                    IsAuthenticated(login, out string token);
                    login.Token = token;
                }

            }

            return login.Adapt<LoginResponse>();
        }

        public RegisterResponse Register(Register register)
        {
            register.Password = Cryptography.Encrypt(register.Password);
            _context.Users.AddAsync(register);
            register.SuccessfullyRegistered = true;
            _context.SaveChanges();
            SetupActivation(register);
            return register.Adapt<RegisterResponse>();
        }

        private void SetupActivation(Register user)
        {

            UserActivation usersActivation = new UserActivation();
            usersActivation.User = user;
            usersActivation.StartTime = DateTime.Now;
            usersActivation.ActivationCode = GenerateActivationCode();
            usersActivation.ActivationType = ActivationType.UserActivation;
            _mailService.SetActivation(usersActivation.Adapt<Activation>());
            _mailService.SetEmailType(EmailType.UserActivation);
            _mailService.SetMailBoxes = ConvertUserToMailBoxesArray(user);
            _mailService.SetEmailMessage();
            _mailService.SendEmailAsync();
            _context.UserActivations.Add(usersActivation);
            _context.SaveChangesAsync();
        }


        private string[] ConvertUserToMailBoxesArray(User user)
        {
            string[] userNameMail = new string[2];
            userNameMail[0] = user.Name + " " + user.Surname;
            userNameMail[1] = user.Email;
            return userNameMail;
        }

        private bool Activate(int UserID, string Code)
        {
            var activation = _context.UserActivations.Where(s => s.User.UserID == UserID).FirstOrDefault();
            if (activation != null)
            { return (activation.ActivationCode == Code); }
            else { return false; }
        }

        private bool SendActivationEmail(int UserID)
        {
            return true;
        }


        private string GenerateActivationCode()
        {
            Random generator = new Random();
            String code = generator.Next(0, 999999).ToString("D6");
            return code;
        }

        private bool IsAuthenticated(Login request, out string token)
        {
            token = string.Empty;
            var claim = new[]
            {
                new Claim(ClaimTypes.Name, request.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                          _tokenManagement.Issuer,
                          _tokenManagement.Audience,
                          claim,
                          expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                          signingCredentials: credentials
                      );

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return true;
        }

        public ActivationResponse ActivateUser(ActivationRequest req)
        {
            req.ActivationCode = Cryptography.Decrypt(req.ActivationCode);
            UserActivation UserAction = _context.UserActivations.Include(x => x.User)
                .Where(p => p.ActivationCode == req.ActivationCode && p.ActivationType == ActivationType.UserActivation)
                .Select(s => s).FirstOrDefault();
            UserAction.User.Active = true;
            UserAction.isActivated = true;
            UserAction.ActivationChannelType = ActivationChannelType.Email;
            _context.SaveChangesAsync();
            return UserAction.Adapt<ActivationResponse>();
        }
        public ResetPasswordResponse SendResetPasswordMail(ResetPasswordRequest req)
        {
            User user = _context.Users.Where(p => p.Email == req.Email && p.Active)
                .Select(s => s).FirstOrDefault();
            UserActivation usersActivation = new UserActivation();
            usersActivation.User = user;
            usersActivation.StartTime = DateTime.Now;
            usersActivation.ActivationCode = GenerateActivationCode();
            usersActivation.ActivationType = ActivationType.PasswordReset;
            _mailService.SetActivation(usersActivation.Adapt<Activation>());
            _mailService.SetEmailType(EmailType.PasswordReset);
            _mailService.SetMailBoxes = ConvertUserToMailBoxesArray(user);
            _mailService.SetEmailMessage();
            _mailService.SendEmailAsync();
            _context.UserActivations.Add(usersActivation);
            _context.SaveChangesAsync();
            ResetPasswordResponse res = new ResetPasswordResponse();
            res.isResetMailSent = true;
            return res;
        }
        public ActivationResponse ResetPassword(ResetPasswordRequest req)
        {
            req.ActivationCode = Cryptography.Decrypt(req.ActivationCode);
            UserActivation UserAction = _context.UserActivations.Include(x => x.User)
                .Where(p => p.ActivationCode == req.ActivationCode && p.ActivationType == ActivationType.PasswordReset)
                .Select(s => s).FirstOrDefault();
            UserAction.User.Password = Cryptography.Encrypt(req.Password);
            UserAction.User.Active = true;
            UserAction.isActivated = true;
            UserAction.ActivationChannelType = ActivationChannelType.Email;
            _context.SaveChangesAsync();
            return UserAction.Adapt<ActivationResponse>();
        }

        public Task<List<User>> GetUsers() => _context.Users.ToListAsync();
    }

  
}
