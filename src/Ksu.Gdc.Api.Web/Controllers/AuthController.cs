using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

using Ksu.Gdc.Api.Core.Configurations;
using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.Entities;

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
        public IActionResult LoginCAS([FromQuery] string service)
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
        public async Task<IActionResult> ValidateCASTicket([FromQuery] string service, [FromQuery] string ticket)
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
                var userId = response.ServiceResponse.AuthenticationSuccess.Attributes.KsuPersonWildcatId;
                DbEntity_User user;
                try
                {
                    user = await _userService.GetByIdAsync(userId);
                }
                catch (NotFoundException)
                {
                    var newUser = new CreateDto_User(response.ServiceResponse.AuthenticationSuccess.Attributes);
                    user = await _userService.CreateAsync(newUser);
                    await _userService.SaveChangesAsync();
                }
                var authDtoUser = Mapper.Map<AuthDto_User>(user);
                var newToken = _authService.BuildToken(authDtoUser);
                Response.Headers.Add("Access-Control-Expose-Headers", "Authorization");
                Response.Headers.Add("Authorization", newToken);
                return Ok(authDtoUser);
            }
            catch (NotAuthorizedException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("cas/logout")]
        public IActionResult LogoutCAS([FromQuery] string service)
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

        [Authorize]
        [HttpGet("validate/token")]
        public async Task<IActionResult> ValidateToken()
        {
            var userId = User.Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => Convert.ToInt32(c.Value))
                .FirstOrDefault();
            var user = await _userService.GetByIdAsync(userId);
            var authDtoUser = Mapper.Map<AuthDto_User>(user);
            var token = Request.Headers
                .Where(h => h.Key == "Authorization")
                .Select(h => h.Value)
                .FirstOrDefault();
            return Ok(authDtoUser);
        }

        private async Task<List<string>> GetUserRoles(int userId)
        {
            var roles = new List<string>();
            if ((await _officerService.GetByUserAsync(userId)).Count > 0)
            {
                roles.Add("officer");
            }
            return roles;
        }
    }
}
