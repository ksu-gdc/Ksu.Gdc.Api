﻿using System;
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
    [Route("portfolio/games")]
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CreateDto_Game newGame)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                var createdGame = await _gameService.CreateAsync(newGame);
                await _gameService.AddCollaboratorAsync(createdGame, newGame.UserId);
                await _gameService.SaveChangesAsync();
                var dtoGame = Mapper.Map<Dto_Game>(createdGame);
                return StatusCode(StatusCodes.Status201Created, dtoGame);
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

        [HttpPost("{gameId}/users")]
        public async Task<IActionResult> AddCollaborator([FromRoute] int gameId, CreateDto_Collaborator collaborator)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                await _gameService.AddCollaboratorAsync(gameId, collaborator.UserId);
                await _gameService.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created);
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

        [HttpGet("")]
        public async Task<IActionResult> Get([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                var games = await _gameService.GetAllAsync();
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

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetById(int gameId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                var game = await _gameService.GetByIdAsync(gameId);
                var dtoGame = Mapper.Map<Dto_Game>(game);
                return Ok(dtoGame);
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

        [HttpGet("featured")]
        public async Task<IActionResult> GetByFeatured([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                var games = await _gameService.GetFeaturedAsync();
                var dtoGames = Mapper.Map<List<Dto_Game>>(games);
                if (PaginatedList.IsValid(pageNumber, pageSize))
                {
                    PaginatedList paginatedGames = new PaginatedList<Dto_Game>(dtoGames, pageNumber, pageSize);
                    return Ok(paginatedGames);
                }
                return Ok(dtoGames);
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

        [HttpGet("{gameId}/thumbnail-image")]
        public async Task<IActionResult> GetImage([FromRoute] int gameId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                var stream = await _gameService.GetImageAsync(gameId);
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

        [HttpGet("{gameId}/users")]
        public async Task<IActionResult> GetCollaborators([FromRoute] int gameId, [FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] bool non)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                List<DbEntity_User> collaborators;
                if (non)
                {
                    collaborators = await _gameService.GetNonCollaboratorsAsync(gameId);
                }
                else
                {
                    collaborators = await _gameService.GetCollaboratorsAsync(gameId);
                }
                var dtoCollaborators = Mapper.Map<List<Dto_User>>(collaborators);
                if (PaginatedList.IsValid(pageNumber, pageSize))
                {
                    PaginatedList paginatedUsers = new PaginatedList<Dto_User>(dtoCollaborators, pageNumber, pageSize);
                    return Ok(paginatedUsers);
                }
                return Ok(dtoCollaborators);
            }
            catch (PaginationException ex)
            {
                return BadRequest(new ErrorResponse(ex));
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new ErrorResponse(ex));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{gameId}")]
        public async Task<IActionResult> UpdateById([FromRoute] int gameId, [FromBody] UpdateDto_Game gameUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                var game = await _gameService.GetByIdAsync(gameId);
                Mapper.Map(gameUpdate, game);
                await _gameService.UpdateAsync(game);
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
            
        [HttpPost("{gameId}/thumbnail-image"), DisableRequestSizeLimit]
        public async Task<IActionResult> UpdateImage([FromRoute] int gameId, [FromForm] IFormFile image)
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
                await _gameService.UpdateImageAsync(gameId, image.OpenReadStream());
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

        [HttpPatch("{gameId}")]
        public async Task<IActionResult> PatchById([FromRoute] int gameId, [FromBody] JsonPatchDocument<UpdateDto_Game> gamePatch)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                var game = await _gameService.GetByIdAsync(gameId);
                var gameUpdate = Mapper.Map<UpdateDto_Game>(game);
                gamePatch.ApplyTo(gameUpdate, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                Mapper.Map(gameUpdate, game);
                await _gameService.UpdateAsync(game);
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

        [HttpDelete("{gameId}")]
        public async Task<IActionResult> DeleteById([FromRoute] int gameId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse(ModelState));
                }
                await _gameService.DeleteByIdAsync(gameId);
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

        [HttpDelete("{gameId}/users/{userId}")]
        public async Task<IActionResult> RemoveCollaborator([FromRoute] int gameId, [FromRoute] int userId)
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
