using System;
using System.Collections.Generic;
using System.Linq;
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
        public readonly IResponse<Login> _response;
        public UserController(IUserManager userManager, IResponse<Login> response)
        {
            _userManager = userManager;
            _response = response;
        }
        [HttpPost("Login")]
        public IResponse<Login> Login(Login login)
        {
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
    }
}
