using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Core.Contracts
{
    public interface IOfficerService
    {
        #region CREATE

        Task<DbEntity_Officer> CreateOfficerAsync(CreateDto_Officer newOfficer);

        #endregion CREATE

        #region GET

        Task<List<DbEntity_Officer>> GetOfficersAsync();

        Task<DbEntity_Officer> GetOfficerByIdAsync(int officerId);

        Task<List<DbEntity_Officer>> GetOfficersByPositionAsync(string position);

        Task<List<DbEntity_Officer>> GetOfficersByUserIdAsync(int userId);

        #endregion GET

        #region UPDATE

        Task<bool> UpdateOfficerAsync(DbEntity_Officer dbOfficer, UpdateDto_Officer updateOfficer);

        #endregion UPDATE

        #region DELETE

        Task<bool> DeleteOfficerByIdAsync(int officerId);

        Task<bool> DeleteOfficersByPositionAsync(string position);

        #endregion DELETE
    }
}
