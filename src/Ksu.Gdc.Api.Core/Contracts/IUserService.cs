using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Core.Contracts
{
    /// <summary>
    /// Users service interface.
    /// </summary>
    public interface IUserService
    {
        #region CREATE

        Task<DbEntity_User> CreateUserAsync(CreateDto_User newUser);

        #endregion CREATE

        #region GET

        Task<List<DbEntity_User>> GetUsersAsync();

        Task<DbEntity_User> GetUserByIdAsync(int userId);

        Task<Stream> GetUserProfileImageAsync(int userId);

        Task<List<DbEntity_Game>> GetGamesOfUserAsync(int userId);

        #endregion GET

        #region UPDATE

        Task<bool> UpdateUserAsync(DbEntity_User dbUser, UpdateDto_User updateUser);

        Task<bool> UpdateUserProfileImageAsync(int userId, Stream imageStream);

        Task<bool> AddGameToUser(int userId, int gameId);

        #endregion UPDATE

        #region DELETE

        Task<bool> RemoveGameFromUser(int userId, int gameId);

        #endregion DELETE
    }
}
