using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Core.Contracts
{
    public interface IOfficerService
    {
        #region CREATE

        Task<ModelEntity_Officer> CreateOfficerAsync(CreateDto_Officer newOfficer);

        #endregion CREATE

        #region GET

        Task<List<ModelEntity_Officer>> GetOfficersAsync();

        Task<ModelEntity_Officer> GetOfficerByIdAsync(int officerId);

        Task<List<ModelEntity_Officer>> GetOfficersByPositionAsync(string position);

        Task<List<ModelEntity_Officer>> GetOfficersByUserIdAsync(int userId);

        #endregion GET

        #region UPDATE

        Task<bool> UpdateOfficerAsync(ModelEntity_Officer dbOfficer, UpdateDto_Officer updateOfficer);

        #endregion UPDATE

        #region DELETE

        Task<bool> DeleteOfficerByIdAsync(int officerId);

        Task<bool> DeleteOfficersByPositionAsync(string position);

        #endregion DELETE
    }
}
