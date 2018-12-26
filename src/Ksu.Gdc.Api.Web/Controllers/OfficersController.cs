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
        [Route("")]
        public async Task<IActionResult> Create([FromBody] CreateDto_Officer newOfficer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var createdOfficer = Mapper.Map<DbEntity_Officer>(newOfficer);
                var dbOfficer = await _officerService.CreateAsync(createdOfficer);
                return StatusCode(StatusCodes.Status201Created, Mapper.Map<Dto_Officer>(dbOfficer));
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

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get([FromQuery] string position)
        {
            try
            {
                List<DbEntity_Officer> officers;
                if (!string.IsNullOrEmpty(position))
                {
                    officers = await _officerService.GetByPositionAsync(position);
                }
                else
                {
                    officers = await _officerService.GetAllAsync();
                }
                return Ok(Mapper.Map<List<Dto_Officer>>(officers));
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

        [HttpGet]
        [Route("{officerId}")]
        public async Task<IActionResult> GetById([FromRoute] int officerId)
        {
            try
            {
                var officer = await _officerService.GetByIdAsync(officerId);
                var dtoOfficer = Mapper.Map<Dto_Officer>(officer);
                return Ok(dtoOfficer);
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
        [Route("{officerId}")]
        public async Task<IActionResult> UpdateById([FromRoute] int officerId, [FromBody] UpdateDto_Officer officerUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var officer = await _officerService.GetByIdAsync(officerId);
                Mapper.Map(officerUpdate, officer);
                await _officerService.UpdateAsync(officer);
                await _officerService.SaveChangesAsync();
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
        [Route("{officerId}")]
        public async Task<IActionResult> PatchById([FromRoute] int officerId, [FromBody] JsonPatchDocument<UpdateDto_Officer> officerPatch)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var officer = await _officerService.GetByIdAsync(officerId);
                var officerUpdate = Mapper.Map<UpdateDto_Officer>(officer);
                officerPatch.ApplyTo(officerUpdate, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Mapper.Map(officerUpdate, officer);
                await _officerService.UpdateAsync(officer);
                await _officerService.SaveChangesAsync();
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
        [Route("")]
        public async Task<IActionResult> Delete([FromQuery] string position)
        {
            try
            {
                if (string.IsNullOrEmpty(position))
                {
                    return BadRequest("The 'position' query parameter is required.");
                }
                else
                {
                    await _officerService.DeleteByPositionAsync(position);
                    await _officerService.SaveChangesAsync();
                }
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
        [Route("{officerId}")]
        public async Task<IActionResult> DeleteById([FromRoute] int officerId)
        {
            try
            {
                await _officerService.DeleteByIdAsync(officerId);
                await _officerService.SaveChangesAsync();
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
