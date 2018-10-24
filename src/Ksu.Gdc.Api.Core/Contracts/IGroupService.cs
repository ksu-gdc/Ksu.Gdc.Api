using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Core.Contracts
{
    public interface IGroupService
    {
        #region CREATE

        Task<ModelEntity_Group> CreateGroupAsync(CreateDto_Group newGroup);

        #endregion CREATE

        #region GET

        Task<List<ModelEntity_Group>> GetGroupsAsync();

        Task<ModelEntity_Group> GetGroupByIdAsync(int groupId);

        Task<List<ModelEntity_User>> GetGroupMembersAsync(int groupId);

        Task<List<ModelEntity_Game>> GetGamesOfGroupAsync(int groupId);

        #endregion GET

        #region UPDATE

        Task<bool> UpdateGroupAsync(ModelEntity_Group dbGroup, UpdateDto_Group updateGroup);

        #endregion UPDATE

        #region DELETE

        Task<bool> DeleteGroupByIdAsync(int groupId);

        #endregion DELETE
    }
}
