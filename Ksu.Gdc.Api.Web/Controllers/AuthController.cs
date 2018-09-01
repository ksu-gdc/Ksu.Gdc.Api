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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public RedirectResult CAS_Login([FromQuery] string returnUrl)
        {
            var serviceUrl = AuthConfig.LoginUrl;
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                serviceUrl = returnUrl;
            }
            var url = $"{AppConfiguration.GetConfig("KsuCas_BaseUrl")}/login?"
                + $"service={serviceUrl}"
                + $"&logoutCallback={AuthConfig.LogoutUrl}"
                + $"&serviceName={AppConfiguration.GetConfig("WebApp_Name")}";
            return Redirect(url);
        }

        [HttpGet]
        [Route("cas/validate", Name = "CAS_Validate")]
        public async Task<IActionResult> CAS_Validate([FromQuery] string ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                return BadRequest();
            }
            using (var client = new HttpClient())
            {
                var url = $"{AppConfiguration.GetConfig("KsuCas_BaseUrl")}/serviceValidate?"
                    + $"service={AuthConfig.LoginUrl}"
                    + $"&ticket={ticket}";
                var response = await client.GetAsync(url);
                return Ok(response);
            }
        }

        [HttpGet]
        [Route("cas/logout", Name = "CAS_Logout")]
        public RedirectResult CAS_Logout([FromQuery] string returnUrl)
        {
            var serviceUrl = AppConfiguration.GetConfig("WebApp_Url");
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                serviceUrl = returnUrl;
            }
            var url = $"{AppConfiguration.GetConfig("KsuCas_BaseUrl")}/logout?"
                + $"url={serviceUrl}";
            return Redirect(url);
        }
    }
}
