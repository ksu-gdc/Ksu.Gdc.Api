using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Models;


namespace Ksu.Gdc.Api.Core.Contracts
{
    public interface IPortfolioService
    {
        #region Interface Methods (Synchronous)

        #region GET

        /// <summary>
        /// Gets the games.
        /// </summary>
        /// <returns>The games.</returns>
        List<Dto_Game> GetGames();

        /// <summary>
        /// Gets the game by identifier.
        /// </summary>
        /// <returns>The game by identifier.</returns>
        /// <param name="gameId">Games identifier.</param>
        Dto_Game GetGameById(int gameId);

        /// <summary>
        /// Gets the game thumbnail image.
        /// </summary>
        /// <returns>The game thumbnail image.</returns>
        /// <param name="gameId">Games identifier.</param>
        Stream GetGameThumbnailImage(int gameId);

        #endregion GET

        #region UPDATE

        /// <summary>
        /// Updates the game thumbnail image.
        /// </summary>
        /// <returns><c>true</c>, if game thumbnail image was updated, <c>false</c> otherwise.</returns>
        /// <param name="gameId">Games identifier.</param>
        /// <param name="imageStream">Image stream.</param>
        bool UpdateGameThumbnailImage(int gameId, Stream imageStream);

        #endregion UPDATE

        #endregion Interface Methods (Synchronous)

        #region Interface Methods (Asynchronous)

        #region GET

        /// <summary>
        /// Gets the games async.
        /// </summary>
        /// <returns>The games async.</returns>
        Task<List<Dto_Game>> GetGamesAsync();

        /// <summary>
        /// Gets the game by identifier async.
        /// </summary>
        /// <returns>The game by identifier async.</returns>
        /// <param name="gameId">Games identifier.</param>
        Task<Dto_Game> GetGameByIdAsync(int gameId);

        /// <summary>
        /// Gets the game thumbnail image async.
        /// </summary>
        /// <returns>The game thumbnail image async.</returns>
        /// <param name="gameId">Games identifier.</param>
        Task<Stream> GetGameThumbnailImageAsync(int gameId);

        #endregion GET

        #region UPDATE

        /// <summary>
        /// Updates the game thumbnail image async.
        /// </summary>
        /// <returns>The game thumbnail image async.</returns>
        /// <param name="gameId">Games identifier.</param>
        /// <param name="imageStream">Image stream.</param>
        Task<bool> UpdateGameThumbnailImageAsync(int gameId, Stream imageStream);

        #endregion UPDATE

        #endregion Interface Methods (Asynchronous)
    }
}
