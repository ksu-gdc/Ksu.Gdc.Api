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
        List<Dto_Game> GetGames();

        /// <summary>
        /// Gets the games async.
        /// </summary>
        /// <returns>The games async.</returns>
        Task<List<Dto_Game>> GetGamesAsync();

        /// <summary>
        /// Gets the games by user identifier.
        /// </summary>
        /// <returns>The games by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
        List<Dto_Game> GetGamesByUserId(int userId);

        /// <summary>
        /// Gets the games by user identifier async.
        /// </summary>
        /// <returns>The games by user identifier async.</returns>
        /// <param name="userId">User identifier.</param>
        Task<List<Dto_Game>> GetGamesByUserIdAsync(int userId);

        /// <summary>
        /// Gets the game by identifier.
        /// </summary>
        /// <returns>The game by identifier.</returns>
        /// <param name="gameId">Game identifier.</param>
        Dto_Game GetGameById(int gameId);

        /// <summary>
        /// Gets the game by identifier async.
        /// </summary>
        /// <returns>The game by identifier async.</returns>
        /// <param name="gameId">Game identifier.</param>
        Task<Dto_Game> GetGameByIdAsync(int gameId);

        /// <summary>
        /// Updates the game thumbnail image.
        /// </summary>
        /// <returns><c>true</c>, if game thumbnail image was updated, <c>false</c> otherwise.</returns>
        /// <param name="gameId">Game identifier.</param>
        /// <param name="imageStream">Image stream.</param>
        bool UpdateGameThumbnailImage(int gameId, Stream imageStream);

        /// <summary>
        /// Updates the game thumbnail image async.
        /// </summary>
        /// <returns>The game thumbnail image async.</returns>
        /// <param name="gameId">Game identifier.</param>
        /// <param name="imageStream">Image stream.</param>
        Task<bool> UpdateGameThumbnailImageAsync(int gameId, Stream imageStream);

        /// <summary>
        /// Gets the game thumbnail image.
        /// </summary>
        /// <returns>The game thumbnail image.</returns>
        /// <param name="gameId">Game identifier.</param>
        Stream GetGameThumbnailImage(int gameId);

        /// <summary>
        /// Gets the game thumbnail image async.
        /// </summary>
        /// <returns>The game thumbnail image async.</returns>
        /// <param name="gameId">Game identifier.</param>
        Task<Stream> GetGameThumbnailImageAsync(int gameId);
    }
}
