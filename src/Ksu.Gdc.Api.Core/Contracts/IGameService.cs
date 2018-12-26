using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Core.Contracts
{
    public interface IGameService
    {
        #region CREATE

        Task<bool> CreateAsync(DbEntity_Game createdGame);
        Task<DbEntity_Game> CreateAsync(CreateDto_Game newGame);

        #endregion CREATE

        #region GET

        Task<List<DbEntity_Game>> GetAllAsync();

        Task<List<DbEntity_Game>> GetFeaturedAsync();

        Task<DbEntity_Game> GetByIdAsync(int gameId);

        Task<Stream> GetImageAsync(DbEntity_Game game);
        Task<Stream> GetImageAsync(int gameId);

        Task<List<DbEntity_User>> GetCollaboratorsAsync(DbEntity_Game game);
        Task<List<DbEntity_User>> GetCollaboratorsAsync(int gameId);

        Task<List<DbEntity_User>> GetNonCollaboratorsAsync(DbEntity_Game game);
        Task<List<DbEntity_User>> GetNonCollaboratorsAsync(int gameId);

        Task<DbEntity_GameUser> GetCollaboratorAsync(int gameId, int userId);

        #endregion GET

        #region UPDATE

        Task<bool> UpdateAsync(DbEntity_Game updatedGame);

        Task<bool> UpdateImageAsync(DbEntity_Game game, Stream image);
        Task<bool> UpdateImageAsync(int gameId, Stream image);

        Task<bool> AddCollaboratorAsync(DbEntity_GameUser collaborator);
        Task<bool> AddCollaboratorAsync(DbEntity_Game game, DbEntity_User user);
        Task<bool> AddCollaboratorAsync(DbEntity_Game game, int userId);
        Task<bool> AddCollaboratorAsync(int gameId, DbEntity_User user);
        Task<bool> AddCollaboratorAsync(int gameId, int userId);

        #endregion UPDATE

        #region DELETE

        Task<bool> DeleteAsync(DbEntity_Game game);

        Task<bool> DeleteByIdAsync(int gameId);

        Task<bool> RemoveCollaboratorAsync(DbEntity_GameUser collaborator);
        Task<bool> RemoveCollaboratorAsync(DbEntity_Game game, DbEntity_User user);
        Task<bool> RemoveCollaboratorAsync(DbEntity_Game game, int userId);
        Task<bool> RemoveCollaboratorAsync(int gameId, DbEntity_User user);
        Task<bool> RemoveCollaboratorAsync(int gameId, int userId);

        #endregion DELETE

        Task<int> SaveChangesAsync();
    }
}
