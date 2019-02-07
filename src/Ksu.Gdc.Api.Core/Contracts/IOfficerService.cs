using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Core.Contracts
{
    public interface IOfficerService
    {
        #region CREATE

        Task<bool> CreateAsync(DbEntity_Officer createdOfficer);

        #endregion CREATE

        #region GET

        Task<List<DbEntity_Officer>> GetAllAsync();

        Task<DbEntity_Officer> GetByIdAsync(int officerId);

        Task<List<DbEntity_Officer>> GetByPositionAsync(string position);

        Task<List<DbEntity_Officer>> GetByUserAsync(DbEntity_User user);
        Task<List<DbEntity_Officer>> GetByUserAsync(int userId);

        #endregion GET

        #region UPDATE

        Task<bool> UpdateAsync(DbEntity_Officer updatedOfficer);

        #endregion UPDATE

        #region DELETE

        Task<bool> DeleteAsync(DbEntity_Officer officer);

        Task<bool> DeleteAsync(List<DbEntity_Officer> officers);

        Task<bool> DeleteByIdAsync(int officerId);

        Task<bool> DeleteByPositionAsync(string position);

        #endregion DELETE

        Task<int> SaveChangesAsync();
    }
}
