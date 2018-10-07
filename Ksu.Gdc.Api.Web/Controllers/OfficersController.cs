using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;

using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.Entities;

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
                List<Dto_Officer> officers;
                if (!string.IsNullOrEmpty(position))
                {
                    officers = await _officerService.GetOfficersByPositionAsync(position);
                }
                else
                {
                    officers = await _officerService.GetOfficersAsync();
                }
                return Ok(officers);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{officerId}", Name = "GetOfficerById")]
        public async Task<IActionResult> GetOfficerById([FromRoute] int officerId)
        {
            try
            {
                var officer = await _officerService.GetOfficerByIdAsync(officerId);
                return Ok(officer);
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

        [HttpPatch]
        [Route("{officerId}")]
        public async Task<IActionResult> UpdateOfficer([FromRoute] int officerId, [FromBody] JsonPatchDocument<Dto_Officer> patch)
        {
            try
            {
                var officer = await _officerService.GetOfficerByIdAsync(officerId);
                patch.ApplyTo(officer, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                //await _officerService.UpdateOfficerUserAsync(officerId);
                return Ok(officer);
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
