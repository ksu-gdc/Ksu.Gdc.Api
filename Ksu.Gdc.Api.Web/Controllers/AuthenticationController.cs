﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Data.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ksu.Gdc.Api.Web.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly Core.Contracts.IAuthenticationService _authService;
        private readonly IUserService _userService;

        public AuthenticationController(Core.Contracts.IAuthenticationService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("cas", Name = "CAS_Login")]
        public async Task<IActionResult> CAS_Login([FromQuery] string returnUrl)
        {
            var properties = new AuthenticationProperties()
            {
                RedirectUri = returnUrl
            };
            await HttpContext.ChallengeAsync("CAS", properties);
            return Ok(Json(User.Identity));
        }
    }
}
