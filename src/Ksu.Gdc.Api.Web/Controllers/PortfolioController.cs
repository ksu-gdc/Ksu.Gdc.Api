using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AutoMapper;

using Ksu.Gdc.Api.Core.Configurations;
using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Web.Controllers
{
    [Route("[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpGet]
        [Route("games", Name = "GetGames")]
        public async Task<IActionResult> GetGames()
        {
            try
            {
                var games = await _portfolioService.GetGamesAsync();
                return Ok(Mapper.Map<List<Dto_Game>>(games));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("games/{id}", Name = "GetGameById")]
        public async Task<IActionResult> GetGameById(int gameId)
        {
            try
            {
                var game = await _portfolioService.GetGameByIdAsync(gameId);
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

        [HttpPost, DisableRequestSizeLimit]
        [Route("games/{id}/thumbnail-image", Name = "UpdateGameThumbnailImage")]
        public async Task<IActionResult> UpdateGameThumbnailImage([FromRoute] int id, [FromForm] IFormFile image)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await _portfolioService.UpdateGameThumbnailImageAsync(id, image.OpenReadStream());
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("games/{id}/thumbnail-image", Name = "GetGameThumbnailImage")]
        public async Task<IActionResult> GetGameThumbnailImage([FromRoute] int id)
        {
            try
            {
                var stream = await _portfolioService.GetGameThumbnailImageAsync(id);
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
    }
}
