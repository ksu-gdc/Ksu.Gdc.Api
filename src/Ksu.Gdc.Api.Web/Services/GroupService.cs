using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;

using Ksu.Gdc.Api.Core.Configurations;
using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.DbContexts;
using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Core.Services
{
    public class GroupService : IGroupService
    {
        private readonly KsuGdcContext _ksuGdcContext;
        private readonly IAmazonS3 _s3Client;

        public GroupService(KsuGdcContext ksuGdcContext, IAmazonS3 s3Client)
        {
            _ksuGdcContext = ksuGdcContext;
            _s3Client = s3Client;
        }

        #region GET

        public async Task<List<ModelEntity_Group>> GetGroupsAsync()
        {
            var dbGroups = await _ksuGdcContext.Groups
                                               .ToListAsync();
            return dbGroups;
        }

        public async Task<ModelEntity_Group> GetGroupByIdAsync(int groupId)
        {
            var dbGroup = await _ksuGdcContext.Groups
                                              .Where(g => g.GroupId == groupId)
                                              .FirstOrDefaultAsync();
            if (dbGroup == null)
            {
                throw new NotFoundException($"No group with id '{groupId}' was found.");
            }
            return dbGroup;
        }

        public async Task<List<ModelEntity_User>> GetGroupMembersAsync(int groupId)
        {
            var dbMembers = await _ksuGdcContext.UserGroup
                                                .Where(g => g.GroupId == groupId)
                                                .Include(ug => ug.User)
                                                .Select(ug => ug.User)
                                                .ToListAsync();
            return dbMembers;
        }

        public async Task<List<ModelEntity_Game>> GetGamesOfGroupAsync(int groupId)
        {
            var dbGames = await _ksuGdcContext.Games
                                              .Where(g => g.GroupId == groupId)
                                              .ToListAsync();
            return dbGames;
        }

        #endregion GET
    }
}
