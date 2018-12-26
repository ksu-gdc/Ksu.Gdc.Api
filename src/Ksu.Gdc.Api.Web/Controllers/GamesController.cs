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

namespace Ksu.Gdc.Api.Web.Controllers
{
    [Route("portfolio/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
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
                return BadRequest(ex.Message);
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
        [Route("{gameId}")]
        public async Task<IActionResult> GetById(int gameId)
        {
            try
            {
                var game = await _gameService.GetByIdAsync(gameId);
                var dtoGame = Mapper.Map<Dto_Game>(game);
                return Ok(dtoGame);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("featured")]
        public async Task<IActionResult> GetByFeatured([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
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
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{gameId}/thumbnail-image")]
        public async Task<IActionResult> GetImage([FromRoute] int gameId)
        {
            try
            {
                var stream = await _gameService.GetImageAsync(gameId);
                return File(stream, "image/jpg");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{gameId}/users")]
        public async Task<IActionResult> GetCollaborators([FromRoute] int gameId, [FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] bool non)
        {
            try
            {
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
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("{gameId}")]
        public async Task<IActionResult> UpdateById([FromRoute] int gameId, [FromBody] UpdateDto_Game gameUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var game = await _gameService.GetByIdAsync(gameId);
                Mapper.Map(gameUpdate, game);
                await _gameService.UpdateAsync(game);
                await _gameService.SaveChangesAsync();
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
        [Route("{gameId}/thumbnail-image")]
        public async Task<IActionResult> UpdateImage([FromRoute] int gameId, [FromForm] IFormFile image)
        {
            try
            {
                if (image.Length == 0)
                {
                    return BadRequest("An image is required.");
                }
                await _gameService.UpdateImageAsync(gameId, image.OpenReadStream());
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch]
        [Route("{gameId}")]
        public async Task<IActionResult> PatchById([FromRoute] int gameId, [FromBody] JsonPatchDocument<UpdateDto_Game> gamePatch)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var game = await _gameService.GetByIdAsync(gameId);
                var gameUpdate = Mapper.Map<UpdateDto_Game>(game);
                gamePatch.ApplyTo(gameUpdate, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Mapper.Map(gameUpdate, game);
                await _gameService.UpdateAsync(game);
                await _gameService.SaveChangesAsync();
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
        [Route("{gameId}")]
        public async Task<IActionResult> DeleteById([FromRoute] int gameId)
        {
            try
            {
                await _gameService.DeleteByIdAsync(gameId);
                await _gameService.SaveChangesAsync();
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
