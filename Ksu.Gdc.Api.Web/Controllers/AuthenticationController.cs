using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ksu.Gdc.Api.Web.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
        public async Task Login([FromQuery] string returnUrl)
        {
            var props = new AuthenticationProperties()
            {
                RedirectUri = returnUrl
            };
            await HttpContext.ChallengeAsync("CAS", props);
        }
    }
}
