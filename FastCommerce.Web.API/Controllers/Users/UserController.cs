using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FastCommerce.Business.UserManager;
using FastCommerce.Entities.Entities;
using FastCommerce.Entities.Models;
using FastCommerce.Web.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastCommerce.Web.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserManager _userManager;
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }



        /// <summary>
        /// Login Method
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>

        [HttpPost("Login")]
        public Response<Login> Login(Login login)
        {
            Response<Login> _response = new Response<Login>();
            try
            {
                _response.RequestState = true;
                _response.Data = _userManager.Login(login);
            }
            catch (Exception ex)
            {
                _response.ErrorState = true;
                _response.Errors.Add(ex);
            }
            return _response;
        }

        /// <summary>
        /// Register Method
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>

        [HttpPost("Register")]
        public Response<Register> Register(Register register)
        {
            Response<Register> _response = new Response<Register>();
            try
            {
                _response.RequestState = true;
                if (register.ValidForRegister())
                {
                    _response.Data = _userManager.Register(register);
                }
                else
                {
                    _response.ErrorState = true;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                }
                    
            }
            catch (Exception ex)
            {
                _response.ErrorState = true;
                _response.Errors.Add(ex);
            }
            return _response;
        }



        /// <summary>
        /// User Activation Method
        /// </summary>
        /// <param name="code">
        /// Activation Key 
        /// </param>
        /// <returns>
        /// <paramref name="response<UsersActivation>"/>
        /// </returns>

        [HttpGet("ActivationBackLink")]
        public Response<UsersActivation> ActivationBackLink(string code)
        {
            Response<UsersActivation> _response = new Response<UsersActivation>();
            try
            {
                _response.RequestState = true;
                if (ModelState.IsValid)
                {
                    _response.Data = _userManager.ActivateUser(code);
                }
                else
                {
                    _response.ErrorState = true;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                }

            }
            catch (Exception ex)
            {
                _response.ErrorState = true;
                _response.Errors.Add(ex);
            }
            return _response;
        }
    }

}
