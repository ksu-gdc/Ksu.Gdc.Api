using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

using Ksu.Gdc.Api.Core.Configurations;
using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Data.Entities;
using Ksu.Gdc.Api.Core.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ksu.Gdc.Api.Web.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
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
                var user = await _authService.ValidateCASTicketAsync(service, ticket);
                return Ok(user);

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
