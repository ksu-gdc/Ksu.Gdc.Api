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
        private readonly IOfficerService _officerService;

        public UsersController(IUserService userService, IOfficerService officerService)
        {
            _userService = userService;
            _officerService = officerService;
        }

        [HttpGet]
        [Route("{userId}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById([FromRoute] int userId)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(userId);
                var dtoUser = Mapper.Map<Dto_User>(user);
                var userOfficers = await _officerService.GetOfficersByUserIdAsync(userId);
                dtoUser.IsOfficer = userOfficers.Count > 0;
                return Ok(dtoUser);
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
        [Route("{userId}/profile-image", Name = "GetUserProfileImage")]
        public async Task<IActionResult> GetUserProfileImage([FromRoute] int userId)
        {
            try
            {
                var stream = await _userService.GetUserProfileImageAsync(userId);
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

        [HttpPost, DisableRequestSizeLimit]
        [Route("{userId}/profile-image", Name = "UpdateUserProfileImage")]
        public async Task<IActionResult> UpdateUserProfileImage([FromRoute] int userId, [FromForm] IFormFile image)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await _userService.UpdateUserProfileImageAsync(userId, image.OpenReadStream());
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("{userId}", Name = "UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromRoute] int userId, [FromBody] UpdateDto_User updateUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var dbUser = await _userService.GetUserByIdAsync(userId);
                await _userService.UpdateUserAsync(dbUser, updateUser);
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
        [Route("{userId}", Name = "PatchUser")]
        public async Task<IActionResult> PatchUser([FromRoute] int userId, [FromBody] JsonPatchDocument<UpdateDto_User> patchUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var dbUser = await _userService.GetUserByIdAsync(userId);
                var updateUser = Mapper.Map<UpdateDto_User>(dbUser);
                patchUser.ApplyTo(updateUser, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await _userService.UpdateUserAsync(dbUser, updateUser);
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
