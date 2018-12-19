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
        [Route("", Name = "GetGames")]
        public async Task<IActionResult> GetGames([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var dbGames = await _gameService.GetGamesAsync();
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

        [HttpGet]
        [Route("featured", Name = "GetFeaturedGames")]
        public async Task<IActionResult> GetFeaturedGames([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var dbGames = await _gameService.GetFeaturedGamesAsync();
                var dtoGames = Mapper.Map<List<Dto_Game>>(dbGames);
                if (PaginatedList.IsValid(pageNumber, pageSize))
                {
                    PaginatedList paginatedGames = new PaginatedList<Dto_Game>(dtoGames, pageNumber, pageSize);
                    return Ok(paginatedGames);
                }
                return Ok(dtoGames);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{gameId}", Name = "GetGameById")]
        public async Task<IActionResult> GetGameById(int gameId)
        {
            try
            {
                var game = await _gameService.GetGameByIdAsync(gameId);
                return Ok(Mapper.Map<Dto_Game>(game));
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
        [Route("{gameId}/thumbnail-image", Name = "GetGameThumbnailImage")]
        public async Task<IActionResult> GetGameThumbnailImage([FromRoute] int gameId)
        {
            try
            {
                var stream = await _gameService.GetGameThumbnailImageAsync(gameId);
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

        [HttpPost, DisableRequestSizeLimit]
        [Route("{gameId}/thumbnail-image", Name = "UpdateGameThumbnailImage")]
        public async Task<IActionResult> UpdateGameThumbnailImage([FromRoute] int gameId, [FromForm] IFormFile image)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _gameService.UpdateGameThumbnailImageAsync(gameId, image.OpenReadStream());
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("{gameId}", Name = "UpdateGame")]
        public async Task<IActionResult> UpdateGame([FromRoute] int gameId, [FromBody] UpdateDto_Game updateGame)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var dbGame = await _gameService.GetGameByIdAsync(gameId);
                await _gameService.UpdateGameAsync(dbGame, updateGame);
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
        [Route("{gameId}", Name = "PatchGame")]
        public async Task<IActionResult> PatchGame([FromRoute] int gameId, [FromBody] JsonPatchDocument<UpdateDto_Game> patchGame)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var dbGame = await _gameService.GetGameByIdAsync(gameId);
                var updateGame = Mapper.Map<UpdateDto_Game>(dbGame);
                patchGame.ApplyTo(updateGame, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _gameService.UpdateGameAsync(dbGame, updateGame);
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
        [Route("{gameId}", Name = "DeleteGameById")]
        public async Task<IActionResult> DeleteGameById([FromRoute] int gameId)
        {
            try
            {
                await _gameService.DeleteGameByIdAsync(gameId);
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
