using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Core.Contracts
{
    public interface IGroupService
    {
        #region CREATE

        Task<DbEntity_Group> CreateGroupAsync(CreateDto_Group newGroup);

        #endregion CREATE

        #region GET

        Task<List<DbEntity_Group>> GetGroupsAsync();

        Task<DbEntity_Group> GetGroupByIdAsync(int groupId);

        Task<List<DbEntity_User>> GetMembersOfGroupAsync(int groupId);

        Task<List<DbEntity_Game>> GetGamesOfGroupAsync(int groupId);

        #endregion GET

        #region UPDATE

        Task<bool> UpdateGroupAsync(DbEntity_Group dbGroup, UpdateDto_Group updateGroup);

        Task<bool> AddMemberToGroupAsync(int groupId, int userId);

        #endregion UPDATE

        #region DELETE

        Task<bool> DeleteGroupByIdAsync(int groupId);

        Task<bool> RemoveMemberFromGroupAsync(int groupId, int userId);

        #endregion DELETE
    }
}
