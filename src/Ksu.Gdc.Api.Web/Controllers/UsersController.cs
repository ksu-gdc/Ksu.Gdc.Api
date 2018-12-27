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
using Ksu.Gdc.Api.Data.Entities;
using Ksu.Gdc.Api.Web.Models;

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
        [Route("{userId}/portfolio/games")]
        public async Task<IActionResult> CreateGameByUser([FromRoute] int userId, [FromBody] CreateDto_Game newGame)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                var createdGame = await _gameService.CreateAsync(newGame);
                await _gameService.AddCollaboratorAsync(createdGame, userId);
                await _gameService.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, Mapper.Map<Dto_Game>(createdGame));
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

        [HttpGet]
        [Route("")]
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

        [HttpGet]
        [Route("{userId}")]
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

        [HttpGet]
        [Route("{userId}/profile-image")]
        public async Task<IActionResult> GetImage([FromRoute] int userId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                var stream = await _userService.GetImageAsync(userId);
                return File(stream, "image/jpg");
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

        [HttpGet]
        [Route("{userId}/portfolio/games")]
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

        [HttpPost, DisableRequestSizeLimit]
        [Route("{userId}/profile-image")]
        public async Task<IActionResult> UpdateImage([FromRoute] int userId, [FromForm] IFormFile image)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                if (image.Length == 0)
                {
                    return BadRequest(new ErrorResponse("An image is required."));
                }
                await _userService.UpdateImageAsync(userId, image.OpenReadStream());
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
        [Route("{userId}")]
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

        [HttpPut]
        [Route("{userId}/portfolio/games/{gameId}")]
        public async Task<IActionResult> AddCollaborator([FromRoute] int userId, [FromRoute] int gameId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                await _gameService.AddCollaboratorAsync(gameId, userId);
                await _gameService.SaveChangesAsync();
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

        [HttpPatch]
        [Route("{userId}")]
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

        [HttpDelete]
        [Route("{userId}/portfolio/games/{gameId}")]
        public async Task<IActionResult> RemoveCollaborator([FromRoute] int userId, [FromRoute] int gameId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                var game = await _gameService.GetByIdAsync(gameId);
                var collaborators = await _gameService.GetCollaboratorsAsync(game);
                if (collaborators.Count <= 1)
                {
                    return Conflict(new ErrorResponse($"Games must have a minimum of 1 collaborator."));
                }
                await _gameService.RemoveCollaboratorAsync(game, userId);
                await _gameService.SaveChangesAsync();
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
