using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ksu.Gdc.Api.Web.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        [HttpGet]
        [Route("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            return Ok();
            //throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{username}", Name = "GetUserByUsername")]
        public async Task<IActionResult> GetUserByUsername([FromRoute] string username)
        {
            throw new NotImplementedException();
        }
    }
}
