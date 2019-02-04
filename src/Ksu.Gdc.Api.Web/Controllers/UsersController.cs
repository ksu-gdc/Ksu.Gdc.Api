using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

using Ksu.Gdc.Api.Core.Configurations;
using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.Entities;
using Ksu.Gdc.Api.Web.Models;

namespace Ksu.Gdc.Api.Web.Controllers
{
    [Authorize]
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IOfficerService _officerService;
        private readonly IGameService _gameService;

        public UsersController(IUserService userService, IOfficerService officerService, IGameService gameService)
        {
            _userService = userService;
            _officerService = officerService;
            _gameService = gameService;
        }

        [AllowAnonymous]
        [HttpGet("")]
        public async Task<IActionResult> Get([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                var users = await _userService.GetAllAsync();
                var dtoUsers = Mapper.Map<List<Dto_User>>(users);
                if (PaginatedList.IsValid(pageNumber, pageSize))
                {
                    PaginatedList paginatedUsers = new PaginatedList<Dto_User>(dtoUsers, pageNumber, pageSize);
                    return Ok(paginatedUsers);
                }
                return Ok(dtoUsers);
            }
            catch (PaginationException ex)
            {
                return BadRequest(new ErrorResponse(ex));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse(ex));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [AllowAnonymous]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById([FromRoute] int userId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                var user = await _userService.GetByIdAsync(userId);
                var dtoUser = Mapper.Map<Dto_User>(user);
                return Ok(dtoUser);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse(ex));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [AllowAnonymous]
        [HttpGet("{userId}/profile-image")]
        public async Task<IActionResult> GetImage([FromRoute] int userId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                var image = await _userService.GetImageAsync(userId);
                return File(image.Data, image.ContentType);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse(ex));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [AllowAnonymous]
        [HttpGet("{userId}/portfolio/games")]
        public async Task<IActionResult> GetGames([FromRoute] int userId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                var games = await _userService.GetGamesAsync(userId);
                var dtoGames = Mapper.Map<List<Dto_Game>>(games);
                if (PaginatedList.IsValid(pageNumber, pageSize))
                {
                    PaginatedList paginatedGames = new PaginatedList<Dto_Game>(dtoGames, pageNumber, pageSize);
                    return Ok(paginatedGames);
                }
                return Ok(dtoGames);
            }
            catch (PaginationException ex)
            {
                return BadRequest(new ErrorResponse(ex));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse(ex));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateById([FromRoute] int userId, [FromBody] UpdateDto_User userUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                var user = await _userService.GetByIdAsync(userId);
                Mapper.Map(userUpdate, user);
                await _userService.UpdateAsync(user);
                await _userService.SaveChangesAsync();
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse(ex));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{userId}/profile-image"), DisableRequestSizeLimit]
        public async Task<IActionResult> UpdateImage([FromRoute] int userId, [FromForm] IFormFile image)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                if (image == null || image.Length == 0)
                {
                    return BadRequest(new ErrorResponse("A valid image is required."));
                }
                var contentType = image.ContentType.Trim().ToLower();
                if (!contentType.StartsWith("image/", StringComparison.CurrentCulture))
                {
                    return BadRequest(new ErrorResponse("Uploaded file must be an image"));
                }
                var stream = new MemoryStream();
                await image.OpenReadStream().CopyToAsync(stream);
                var imageUpdate = new UpdateDto_Image()
                {
                    Name = "user_profile",
                    Data = stream.ToArray(),
                    ContentType = contentType
                };
                await _userService.UpdateImageAsync(userId, imageUpdate);
                await _userService.SaveChangesAsync();
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{userId}")]
        public async Task<IActionResult> PatchById([FromRoute] int userId, [FromBody] JsonPatchDocument<UpdateDto_User> userPatch)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                var user = await _userService.GetByIdAsync(userId);
                var userUpdate = Mapper.Map<UpdateDto_User>(user);
                userPatch.ApplyTo(userUpdate, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                Mapper.Map(userUpdate, user);
                await _userService.UpdateAsync(user);
                await _userService.SaveChangesAsync();
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ErrorResponse(ex));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
