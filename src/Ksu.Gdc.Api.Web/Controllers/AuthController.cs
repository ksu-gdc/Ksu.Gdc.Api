using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using AutoMapper;

using Ksu.Gdc.Api.Core.Configurations;
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
        private readonly IOfficerService _officerService;

        public AuthController(IAuthService authService, IUserService userService, IOfficerService officerService)
        {
            _authService = authService;
            _userService = userService;
            _officerService = officerService;
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
                if (string.IsNullOrWhiteSpace(service))
                {
                    return BadRequest();
                }
                if (string.IsNullOrWhiteSpace(ticket))
                {
                    throw new NotAuthorizedException();
                }
                var response = await _authService.ValidateCASTicketAsync(service, ticket);
                try
                {
                    var userId = response.ServiceResponse.AuthenticationSuccess.Attributes.KsuPersonWildcatId[0];
                    var dbUser = await _userService.GetUserByIdAsync(userId);
                    var dtoUser = Mapper.Map<AuthDto_User>(dbUser);
                    if ((await _officerService.GetOfficersByUserIdAsync(userId)).Count > 0)
                    {
                        dtoUser.Roles.Add("officer");
                    }
                    return Ok(dtoUser);
                }
                catch (NotFoundException)
                {
                    var newUser = new CreateDto_User(response.ServiceResponse.AuthenticationSuccess.Attributes);
                    var dbUser = await _userService.CreateUserAsync(newUser);
                    return StatusCode(StatusCodes.Status201Created, Mapper.Map<Dto_User>(dbUser));
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
