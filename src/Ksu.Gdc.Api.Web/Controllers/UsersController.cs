using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;

using Ksu.Gdc.Api.Core.Configurations;
using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Core.Models;

namespace Ksu.Gdc.Api.Web.Controllers
{
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return Ok(Mapper.Map<Dto_User>(user));
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
        [Route("{userId}", Name = "UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromRoute] int userId, [FromBody] UpdateDto_User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await _userService.UpdateUserAsync(userId, user);
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

        [HttpPost, DisableRequestSizeLimit]
        [Route("{id}/profile-image", Name = "UpdateUserProfileImage")]
        public async Task<IActionResult> UpdateUserProfileImage([FromRoute] int id, [FromForm] IFormFile image)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await _userService.UpdateUserProfileImageAsync(id, image.OpenReadStream());
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id}/profile-image", Name = "GetUserProfileImage")]
        public async Task<IActionResult> GetUserProfileImage([FromRoute] int id)
        {
            try
            {
                var stream = await _userService.GetUserProfileImageAsync(id);
                return File(stream, "image/jpg");
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
        [Route("{userId}/groups", Name = "GetGroupsOfUser")]
        public async Task<IActionResult> GetGroupsOfUser([FromRoute] int userId)
        {
            try
            {
                var groups = await _userService.GetGroupsOfUserAsync(userId);
                return Ok(Mapper.Map<List<Dto_Group>>(groups));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{userId}/portfolio/games", Name = "GetGamesOfUser")]
        public async Task<IActionResult> GetGamesOfUser([FromRoute] int userId)
        {
            try
            {
                var games = await _userService.GetGamesOfUserAsync(userId);
                return Ok(Mapper.Map<List<Dto_Game>>(games));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
