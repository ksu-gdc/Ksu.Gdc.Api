using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Ksu.Gdc.Api.Core.Contracts;

namespace Ksu.Gdc.Api.Web.Controllers
{
    [Route("")]
    public class UtilityController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult KeepAwake()
        {
            try
            {
                return Ok("API is awake!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
