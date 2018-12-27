using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Ksu.Gdc.Api.Web.Models;

namespace Ksu.Gdc.Api.Web.Controllers
{
    [Route("")]
    public class UtilityController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> KeepAwake()
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
