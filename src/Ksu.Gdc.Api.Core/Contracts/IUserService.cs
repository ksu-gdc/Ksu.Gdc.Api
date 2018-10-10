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
        #region GET

        Task<ModelEntity_User> GetUserByIdAsync(int userId);

        Task<Stream> GetUserProfileImageAsync(int userId);

        Task<List<ModelEntity_Group>> GetGroupsOfUserAsync(int userId);

        Task<List<ModelEntity_Game>> GetGamesOfUserAsync(int userId);

        #endregion GET

        #region ADD

        Task<bool> AddUserAsync(CreateDto_User newUser);

        #endregion ADD

        #region UPDATE

        Task<bool> UpdateUserAsync(int userId, UpdateDto_User user);

        Task<bool> UpdateUserProfileImageAsync(int userId, Stream imageStream);

        #endregion UPDATE
    }
}
