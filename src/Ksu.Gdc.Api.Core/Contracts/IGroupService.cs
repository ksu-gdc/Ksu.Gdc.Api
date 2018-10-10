using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Core.Contracts
{
    public interface IGroupService
    {
        #region GET

        Task<List<ModelEntity_Group>> GetGroupsAsync();

        Task<ModelEntity_Group> GetGroupByIdAsync(int groupId);

        Task<List<ModelEntity_User>> GetGroupMembersAsync(int groupId);

        Task<List<ModelEntity_Game>> GetGamesOfGroupAsync(int groupId);

        #endregion GET
    }
}
