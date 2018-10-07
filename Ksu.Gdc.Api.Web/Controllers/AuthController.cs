using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

using Ksu.Gdc.Api.Configuration;
using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Data.Entities;
using Ksu.Gdc.Api.Core.Models;

namespace Ksu.Gdc.Api.Web.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("cas/login", Name = "CAS_Login")]
        public IActionResult CAS_Login([FromQuery] string service)
        {
            if (string.IsNullOrWhiteSpace(service))
            {
                return BadRequest();
            }
            var url = $"{AppConfiguration.GetConfig("KsuCas_BaseUrl")}/login?"
                + $"service={service}"
                + $"&logoutCallback={AuthConfig.LogoutUrl}"
                + $"&serviceName={AppConfiguration.GetConfig("App_Name")}";
            return Redirect(url);
        }

        [HttpGet]
        [Route("cas/validate", Name = "CAS_Validate")]
        public async Task<IActionResult> CAS_Validate([FromQuery] string service, [FromQuery] string ticket)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ticket) || string.IsNullOrWhiteSpace(service))
                {
                    return BadRequest();
                }
                var response = await _authService.ValidateCASTicketAsync(service, ticket);
                try
                {
                    var userId = response.ServiceResponse.AuthenticationSuccess.Attributes.KsuPersonWildcatId[0];
                    var dtoUser = await _userService.GetUserByIdAsync(userId);
                    return Ok(dtoUser);
                }
                catch (NotFoundException)
                {
                    var newUser = new CreateDto_User(response.ServiceResponse.AuthenticationSuccess.Attributes);
                    var dtoUser = await _userService.AddUserAsync(newUser);
                    return StatusCode(StatusCodes.Status201Created, dtoUser);
                }

            }
            catch (NotAuthorizedException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("cas/logout", Name = "CAS_Logout")]
        public IActionResult CAS_Logout([FromQuery] string service)
        {
            if (string.IsNullOrWhiteSpace(service))
            {
                service = AuthConfig.LogoutUrl;
            }
            var url = $"{AppConfiguration.GetConfig("KsuCas_BaseUrl")}/logout?"
                + $"url={service}";
            return Redirect(url);
        }
    }
}
