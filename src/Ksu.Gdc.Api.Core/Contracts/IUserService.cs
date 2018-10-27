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

        Task<ModelEntity_User> CreateUserAsync(CreateDto_User newUser);

        #endregion CREATE

        #region GET

        Task<List<ModelEntity_User>> GetUsersAsync();

        Task<ModelEntity_User> GetUserByIdAsync(int userId);

        Task<Stream> GetUserProfileImageAsync(int userId);

        Task<List<ModelEntity_Group>> GetGroupsOfUserAsync(int userId);

        Task<List<ModelEntity_Game>> GetGamesOfUserAsync(int userId);

        #endregion GET

        #region UPDATE

        Task<bool> UpdateUserAsync(ModelEntity_User dbUser, UpdateDto_User updateUser);

        Task<bool> UpdateUserProfileImageAsync(int userId, Stream imageStream);

        #endregion UPDATE

        #region DELETE

        Task<bool> DeleteUserByIdAsync(int userId);

        #endregion DELETE
    }
}
