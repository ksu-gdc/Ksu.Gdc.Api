using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;

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
                List<ModelEntity_Officer> officers;
                if (!string.IsNullOrEmpty(position))
                {
                    officers = await _officerService.GetOfficersByPositionAsync(position);
                }
                else
                {
                    officers = await _officerService.GetOfficersAsync();
                }
                return Ok(Mapper.Map<List<Dto_Officer>>(officers));
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
                return Ok(Mapper.Map<Dto_Officer>(officer));
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
        [Route("{officerId}", Name = "UpdateOfficer")]
        public async Task<IActionResult> UpdateOfficerUser([FromRoute] int officerId, [FromBody] JsonPatchDocument<UpdateDto_Officer> patchOfficer)
        {
            try
            {
                var officer = await _officerService.GetOfficerByIdAsync(officerId);
                var updateOfficer = Mapper.Map<UpdateDto_Officer>(officer);
                patchOfficer.ApplyTo(updateOfficer, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                return Ok(officer);
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest();
                //}
                //await _officerService.UpdateOfficerAsync(officerId, officer);
                //return Ok();
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
