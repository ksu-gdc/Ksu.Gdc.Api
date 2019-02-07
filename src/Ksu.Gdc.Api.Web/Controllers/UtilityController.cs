using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Ksu.Gdc.Api.Web.Models;

namespace Ksu.Gdc.Api.Web.Controllers
{
    public class UtilityController : Controller
    {
        [HttpGet("")]
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
