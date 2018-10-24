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

        [HttpPost]
        [Route("", Name = "CreateOfficer")]
        public async Task<IActionResult> CreateOfficer([FromBody] CreateDto_Officer newOfficer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var dbOfficer = await _officerService.CreateOfficerAsync(newOfficer);
                return StatusCode(StatusCodes.Status201Created, Mapper.Map<Dto_Officer>(dbOfficer));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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

        [HttpPut]
        [Route("{officerId}", Name = "UpdateOfficer")]
        public async Task<IActionResult> UpdateOfficer([FromRoute] int officerId, [FromBody] UpdateDto_Officer updateOfficer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var dbOfficer = await _officerService.GetOfficerByIdAsync(officerId);
                await _officerService.UpdateOfficerAsync(dbOfficer, updateOfficer);
                return Ok();
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
        [Route("{officerId}", Name = "PatchOfficer")]
        public async Task<IActionResult> PatchOfficer([FromRoute] int officerId, [FromBody] JsonPatchDocument<UpdateDto_Officer> patchOfficer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var dbOfficer = await _officerService.GetOfficerByIdAsync(officerId);
                var updateOfficer = Mapper.Map<UpdateDto_Officer>(dbOfficer);
                patchOfficer.ApplyTo(updateOfficer, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await _officerService.UpdateOfficerAsync(dbOfficer, updateOfficer);
                return Ok();
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

        [HttpDelete]
        [Route("", Name = "DeleteOfficers")]
        public async Task<IActionResult> DeleteOfficers([FromQuery] string position)
        {
            try
            {
                if (!string.IsNullOrEmpty(position))
                {
                    await _officerService.DeleteOfficersByPositionAsync(position);
                }
                else
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("{officerId}", Name = "DeleteOfficerById")]
        public async Task<IActionResult> DeleteOfficerById([FromRoute] int officerId)
        {
            try
            {
                await _officerService.DeleteOfficerByIdAsync(officerId);
                return Ok();
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
