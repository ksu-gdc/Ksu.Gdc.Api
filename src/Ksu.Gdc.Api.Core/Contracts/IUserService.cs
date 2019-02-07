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

        Task<bool> CreateAsync(DbEntity_User createdUser);
        Task<DbEntity_User> CreateAsync(CreateDto_User newUser);

        #endregion CREATE

        #region GET

        Task<List<DbEntity_User>> GetAllAsync();

        Task<DbEntity_User> GetByIdAsync(int userId);

        Task<DbEntity_Image> GetImageAsync(DbEntity_User user);
        Task<DbEntity_Image> GetImageAsync(int userId);

        Task<List<DbEntity_Game>> GetGamesAsync(DbEntity_User user);
        Task<List<DbEntity_Game>> GetGamesAsync(int userId);

        #endregion GET

        #region UPDATE

        Task<bool> UpdateAsync(DbEntity_User updatedUser);

        Task<bool> UpdateImageAsync(int userId, UpdateDto_Image imageUpdate);

        #endregion UPDATE

        #region DELETE

        #endregion DELETE

        Task<int> SaveChangesAsync();
    }
}
