using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Data.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ksu.Gdc.Api.Web.Controllers
{
    [Route("[controller]")]
    public class OfficersController : ControllerBase
    {
        private readonly IOfficerService _officerService;

        public OfficersController(IOfficerService officerService)
        {
            _officerService = officerService;
        }

        [HttpGet]
        [Route("", Name = "GetOfficers")]
        public async Task<IActionResult> GetOfficers([FromQuery] string position)
        {
            try
            {
                if (!string.IsNullOrEmpty(position))
                {
                    var officers = await _officerService.GetOfficersByPositionAsync(position);
                    return Ok(officers);
                }
                else
                {
                    var officers = await _officerService.GetOfficersAsync();
                    return Ok(officers);
                }
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
