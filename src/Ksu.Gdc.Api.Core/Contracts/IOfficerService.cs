using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Core.Contracts
{
    public interface IOfficerService
    {
        #region GET

        Task<List<ModelEntity_Officer>> GetOfficersAsync();

        Task<ModelEntity_Officer> GetOfficerByIdAsync(int officerId);

        Task<List<ModelEntity_Officer>> GetOfficersByPositionAsync(string position);

        #endregion GET

        #region UPDATE

        Task<bool> UpdateOfficerAsync(int officerId, UpdateDto_Officer updateOfficer);

        #endregion UPDATE

        #region DELETE

        Task<bool> DeleteOfficerAsync(int officerId);

        Task<bool> DeleteOfficersAsync(string position);

        #endregion DELETE
    }
}
