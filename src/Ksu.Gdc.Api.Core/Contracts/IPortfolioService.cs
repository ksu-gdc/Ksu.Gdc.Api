using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Core.Contracts
{
    public interface IPortfolioService
    {
        #region GET

        Task<List<ModelEntity_Game>> GetGamesAsync();

        Task<ModelEntity_Game> GetGameByIdAsync(int gameId);

        Task<Stream> GetGameThumbnailImageAsync(int gameId);

        #endregion GET

        #region UPDATE

        Task<bool> UpdateGameThumbnailImageAsync(int gameId, Stream imageStream);

        #endregion UPDATE
    }
}
