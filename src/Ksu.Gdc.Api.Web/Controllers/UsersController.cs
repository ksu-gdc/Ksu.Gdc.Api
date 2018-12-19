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
        private readonly IGameService _gameService;

        public UsersController(IUserService userService, IOfficerService officerService, IGameService gameService)
        {
            _userService = userService;
            _officerService = officerService;
            _gameService = gameService;
        }

        [HttpPost]
        [Route("{userId}/portfolio/games", Name = "CreateUserGame")]
        public async Task<IActionResult> CreateUserGame([FromRoute] int userId, [FromBody] CreateDto_Game newGame)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var dbGame = await _gameService.CreateGameAsync(newGame);
                await _userService.AddGameToUser(userId, dbGame.GameId);
                return StatusCode(StatusCodes.Status201Created, Mapper.Map<Dto_Game>(dbGame));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("", Name = "GetUsers")]
        public async Task<IActionResult> GetUsers([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var dbUsers = await _userService.GetUsersAsync();
                var dtoUsers = Mapper.Map<List<Dto_User>>(dbUsers);
                if (PaginatedList.IsValid(pageNumber, pageSize))
                {
                    PaginatedList paginatedUsers = new PaginatedList<Dto_User>(dtoUsers, pageNumber, pageSize);
                    return Ok(paginatedUsers);
                }
                return Ok(dtoUsers);
            }
            catch (PaginationException ex)
            {
                ModelState.AddModelError("Pagination", ex.Message);
                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{userId}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById([FromRoute] int userId)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(userId);
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
        [Route("{userId}/portfolio/games", Name = "GetGamesOfUser")]
        public async Task<IActionResult> GetGamesOfUser([FromRoute] int userId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var dbGames = await _userService.GetGamesOfUserAsync(userId);
                var dtoGames = Mapper.Map<List<Dto_Game>>(dbGames);
                if (PaginatedList.IsValid(pageNumber, pageSize))
                {
                    PaginatedList paginatedGames = new PaginatedList<Dto_Game>(dtoGames, pageNumber, pageSize);
                    return Ok(paginatedGames);
                }
                return Ok(dtoGames);
            }
            catch (PaginationException ex)
            {
                ModelState.AddModelError("Pagination", ex.Message);
                return BadRequest(ModelState);
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
                    return BadRequest(ModelState);
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
                    return BadRequest(ModelState);
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

        [HttpPut]
        [Route("{userId}/games/{gameId}", Name = "AddGameToUser")]
        public async Task<IActionResult> AddGameToUser([FromRoute] int userId, [FromRoute] int gameId)
        {
            try
            {
                var game = await _gameService.GetGameByIdAsync(gameId);
                var user = await _userService.GetUserByIdAsync(userId);
                await _userService.AddGameToUser(user.UserId, game.GameId);
                return Ok();
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
                    return BadRequest(ModelState);
                }
                var dbUser = await _userService.GetUserByIdAsync(userId);
                var updateUser = Mapper.Map<UpdateDto_User>(dbUser);
                patchUser.ApplyTo(updateUser, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
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

        [HttpDelete]
        [Route("{userId}/games/{gameId}", Name = "RemoveGameFromUser")]
        public async Task<IActionResult> RemoveGameFromUser([FromRoute] int userId, [FromRoute] int gameId)
        {
            try
            {
                var game = await _gameService.GetGameByIdAsync(gameId);
                var user = await _userService.GetUserByIdAsync(userId);
                await _userService.RemoveGameFromUser(gameId, userId);
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
