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
        public Response<LoginResponse> Login(Login login)
        {
            Response<LoginResponse> _response = new Response<LoginResponse>();
            try
            {
                _response.RequestState = true;
                _response.Data = _userManager.Login(login);
            }
            catch (Exception ex)
            {
                _response.ErrorState = true;
                _response.ErrorList.Add(ex);
            }
            return _response;
        }

        /// <summary>
        /// Register Method
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>

        [HttpPost("Register")]
        public Response<RegisterResponse> Register(Register register)
        {
            Response<RegisterResponse> _response = new Response<RegisterResponse>();
            try
            {
                _response.RequestState = true;
                _response.Data = _userManager.Register(register);
                    
            }
            catch (Exception ex)
            {
                _response.ErrorState = true;
                _response.ErrorList.Add(ex);
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
        public Response<UserActivation> ActivationBackLink(string code)
        {
            Response<UserActivation> _response = new Response<UserActivation>();
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
                _response.ErrorList.Add(ex);
            }
            return _response;
        }
    }

}
