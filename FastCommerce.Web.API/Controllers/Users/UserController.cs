using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FastCommerce.Business.DTOs.User;
using FastCommerce.Business.UserManager;
using FastCommerce.Business.UserManager.Abstract;
using FastCommerce.Entities.Entities;
using FastCommerce.Entities.Models;
using FastCommerce.Web.API.Models;
using Mapster;
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
                _response.ErrorList.Add(ex.Adapt<ApiException>());
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
                _response.ErrorList.Add(ex.Adapt<ApiException>());
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

        [HttpPost("ActivationBackLink")]
        public Response<ActivationResponse> ActivationBackLink(ActivationRequest req)
        {
            Response<ActivationResponse> _response = new Response<ActivationResponse>();
            try
            {
                _response.RequestState = true;
                if (ModelState.IsValid)
                {
                    _response.Data = _userManager.ActivateUser(req);
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
                _response.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return _response;
        }
        [HttpPost("ResetPassword")]
        public Response<ResetPasswordResponse> ResetPassword(ResetPasswordRequest req)
        {
            Response<ResetPasswordResponse> _response = new Response<ResetPasswordResponse>();
            try
            {
                _response.RequestState = true;
                if (ModelState.IsValid)
                {
                    _response.Data = _userManager.SendResetPasswordMail(req);
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
                _response.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return _response;
        }
        [HttpPost("ResetPasswordBacklink")]
        public Response<ActivationResponse> ResetPasswordBacklink(ResetPasswordRequest req)
        {
            Response<ActivationResponse> _response = new Response<ActivationResponse>();
            try
            {
                _response.RequestState = true;
                if (ModelState.IsValid)
                {
                    _response.Data = _userManager.ResetPassword(req);
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
                _response.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return _response;
        }

        /// <summary>
        /// Get Method
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpGet("Get")]
        public async Task<HttpResponseMessage> Get()
        {
            Response<User> _response = new Response<User>();
            try
            {
                _response.RequestState = true;
                _response.DataList = await _userManager.GetUsers();
                _response.ErrorState = false;
            }
            catch (Exception ex)
            {
                _response.ErrorState = true;
                _response.ErrorList.Add(ex.Adapt<ApiException>());
            }
            return _response;
        }
    }

}
