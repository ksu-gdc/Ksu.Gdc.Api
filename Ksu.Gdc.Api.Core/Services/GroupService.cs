using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;

using Ksu.Gdc.Api.Configuration;
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

        #region Interface Methods (Synchronous)

        #region GET

        public List<Dto_Group> GetGroups()
        {
            return GetGroupsAsync().Result;
        }

        public Dto_Group GetGroupById(int groupId)
        {
            return GetGroupByIdAsync(groupId).Result;
        }

        public List<Dto_User> GetGroupMembers(int groupId)
        {
            return GetGroupMembersAsync(groupId).Result;
        }

        public List<Dto_Game> GetGamesOfGroup(int groupId)
        {
            return GetGamesOfGroupAsync(groupId).Result;
        }

        #endregion GET

        #endregion Interface Methods (Synchronous)

        #region Interface Methods (Asynchronous)

        #region GET

        public async Task<List<Dto_Group>> GetGroupsAsync()
        {
            var dbGroups = await _ksuGdcContext.Groups
                                               .ToListAsync();
            var dtoGroups = Mapper.Map<List<Dto_Group>>(dbGroups);
            return dtoGroups;
        }

        public async Task<Dto_Group> GetGroupByIdAsync(int groupId)
        {
            var dbGroup = await _ksuGdcContext.Groups
                                              .Where(g => g.GroupId == groupId)
                                              .FirstOrDefaultAsync();
            if (dbGroup == null)
            {
                throw new NotFoundException($"No group with id '{groupId}' was found.");
            }
            var dtoGroup = Mapper.Map<Dto_Group>(dbGroup);
            return dtoGroup;
        }

        public async Task<List<Dto_User>> GetGroupMembersAsync(int groupId)
        {
            var dbMembers = await _ksuGdcContext.UserGroup
                                                .Where(g => g.GroupId == groupId)
                                                .Include(ug => ug.User)
                                                .Select(ug => ug.User)
                                                .ToListAsync();
            var dtoMembers = Mapper.Map<List<Dto_User>>(dbMembers);
            return dtoMembers;
        }

        public async Task<List<Dto_Game>> GetGamesOfGroupAsync(int groupId)
        {
            var dbGames = await _ksuGdcContext.Games
                                              .Where(g => g.GroupId == groupId)
                                              .ToListAsync();
            var dtoGames = Mapper.Map<List<Dto_Game>>(dbGames);
            return dtoGames;
        }

        #endregion GET

        #endregion Interface Methods (Asynchronous)
    }
}
