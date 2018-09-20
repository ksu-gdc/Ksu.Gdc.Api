using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Models;


namespace Ksu.Gdc.Api.Core.Contracts
{
    public interface IPortfolioService
    {
        /// <summary>
        /// Gets the games.
        /// </summary>
        /// <returns>The games.</returns>
        List<GameDto> GetGames();

        /// <summary>
        /// Gets the games async.
        /// </summary>
        /// <returns>The games async.</returns>
        Task<List<GameDto>> GetGamesAsync();

        /// <summary>
        /// Gets the game by identifier.
        /// </summary>
        /// <returns>The game by identifier.</returns>
        /// <param name="id">Identifier.</param>
        GameDto GetGameById(int id);

        /// <summary>
        /// Gets the game by identifier async.
        /// </summary>
        /// <returns>The game by identifier async.</returns>
        /// <param name="id">Identifier.</param>
        Task<GameDto> GetGameByIdAsync(int id);

        /// <summary>
        /// Gets the game thumbnail image.
        /// </summary>
        /// <returns>The game thumbnail image.</returns>
        /// <param name="id">Identifier.</param>
        Stream GetGameThumbnailImage(int id);

        /// <summary>
        /// Gets the game thumbnail image async.
        /// </summary>
        /// <returns>The game thumbnail image async.</returns>
        /// <param name="id">Identifier.</param>
        Task<Stream> GetGameThumbnailImageAsync(int id);
    }
}
