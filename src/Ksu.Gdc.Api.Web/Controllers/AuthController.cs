using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

using Ksu.Gdc.Api.Core.Configurations;
using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Core.Models;

namespace Ksu.Gdc.Api.Web.Controllers
{
    [Route("auth")]
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

        [HttpGet("cas/login")]
        public IActionResult CAS_Login([FromQuery] string service)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(service))
                {
                    service = AuthConfig.LoginUrl;
                }
                var url = $"{AppConfiguration.GetConfig("KsuCas_BaseUrl")}/login?"
                    + $"service={service}"
                    + $"&logoutCallback={AuthConfig.LogoutUrl}"
                    + $"&serviceName={AppConfiguration.GetConfig("App_Name")}";
                return Redirect(url);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("cas/validate")]
        public async Task<IActionResult> CAS_Validate([FromQuery] string service, [FromQuery] string ticket)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(service))
                {
                    service = AuthConfig.LoginUrl;
                }
                if (string.IsNullOrWhiteSpace(ticket))
                {
                    throw new NotAuthorizedException();
                }
                var response = await _authService.ValidateCASTicketAsync(service, ticket);
                try
                {
                    var userId = response.ServiceResponse.AuthenticationSuccess.Attributes.KsuPersonWildcatId;
                    var user = await _userService.GetByIdAsync(userId);
                    var authDtoUser = Mapper.Map<AuthDto_User>(user);
                    if ((await _officerService.GetByUserAsync(userId)).Count > 0)
                    {
                        authDtoUser.Roles.Add("officer");
                    }
                    authDtoUser.Token = _authService.BuildToken(authDtoUser);
                    return Ok(authDtoUser);
                }
                catch (NotFoundException)
                {
                    var newUser = new CreateDto_User(response.ServiceResponse.AuthenticationSuccess.Attributes);
                    var createdUser = await _userService.CreateAsync(newUser);
                    await _userService.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status201Created, Mapper.Map<Dto_User>(createdUser));
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

        [HttpGet("cas/logout")]
        public IActionResult CAS_Logout([FromQuery] string service)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(service))
                {
                    service = AuthConfig.LogoutUrl;
                }
                var url = $"{AppConfiguration.GetConfig("KsuCas_BaseUrl")}/logout?"
                    + $"url={service}";
                return Redirect(url);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
