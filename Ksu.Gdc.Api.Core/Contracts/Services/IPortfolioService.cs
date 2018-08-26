using System;
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
    }
}
