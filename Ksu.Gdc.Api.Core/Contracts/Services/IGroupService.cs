using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Models;

namespace Ksu.Gdc.Api.Core.Contracts
{
    public interface IGroupService
    {
        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <returns>The groups.</returns>
        List<Dto_Group> GetGroups();

        /// <summary>
        /// Gets the groups async.
        /// </summary>
        /// <returns>The groups async.</returns>
        Task<List<Dto_Group>> GetGroupsAsync();

        /// <summary>
        /// Gets the groups by user identifier.
        /// </summary>
        /// <returns>The groups by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
        List<Dto_Group> GetGroupsByUserId(int userId);

        /// <summary>
        /// Gets the groups by user identifier async.
        /// </summary>
        /// <returns>The groups by user identifier async.</returns>
        /// <param name="userId">User identifier.</param>
        Task<List<Dto_Group>> GetGroupsByUserIdAsync(int userId);

        /// <summary>
        /// Gets the group by identifier.
        /// </summary>
        /// <returns>The group by identifier.</returns>
        /// <param name="groupId">Group identifier.</param>
        Dto_Group GetGroupById(int groupId);

        /// <summary>
        /// Gets the group by identifier async.
        /// </summary>
        /// <returns>The group by identifier async.</returns>
        /// <param name="groupId">Group identifier.</param>
        Task<Dto_Group> GetGroupByIdAsync(int groupId);

        /// <summary>
        /// Gets the members by group identifier.
        /// </summary>
        /// <returns>The members by group identifier.</returns>
        /// <param name="groupId">Group identifier.</param>
        List<Dto_User> GetGroupMembersByGroupId(int groupId);

        /// <summary>
        /// Gets the members by group identifier async.
        /// </summary>
        /// <returns>The members by group identifier async.</returns>
        /// <param name="groupId">Group identifier.</param>
        Task<List<Dto_User>> GetGroupMembersByGroupIdAsync(int groupId);
    }
}
