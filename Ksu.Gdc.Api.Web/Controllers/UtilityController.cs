using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ksu.Gdc.Api.Web.Controllers
{
    [Route("")]
    public class UtilityController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult KeepAwake()
        {
            return Ok("API is awake!");
        }
    }
}
