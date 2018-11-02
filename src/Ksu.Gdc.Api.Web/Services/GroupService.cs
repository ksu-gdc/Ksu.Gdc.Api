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
using AutoMapper;

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

        #region CREATE

        public async Task<DbEntity_Group> CreateGroupAsync(CreateDto_Group newGroup)
        {
            var newDbGroup = Mapper.Map<DbEntity_Group>(newGroup);
            await _ksuGdcContext.Groups.AddAsync(newDbGroup);
            await _ksuGdcContext.SaveChangesAsync();
            return newDbGroup;
        }

        #endregion CREATE

        #region GET

        public async Task<List<DbEntity_Group>> GetGroupsAsync()
        {
            var dbGroups = await _ksuGdcContext.Groups
                                               .ToListAsync();
            return dbGroups;
        }

        public async Task<DbEntity_Group> GetGroupByIdAsync(int groupId)
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

        public async Task<Stream> GetGroupProfileImageAsync(int groupId)
        {
            try
            {
                var transferUtility = new TransferUtility(_s3Client);
                var transferRequest = new TransferUtilityOpenStreamRequest()
                {
                    BucketName = AppConfiguration.GetConfig("AWS_S3_BucketName"),
                    Key = $"{GroupConfig.DataStoreDirPath}/{groupId}/profile.jpg"
                };
                var stream = await transferUtility.OpenStreamAsync(transferRequest);
                return stream;
            }
            catch (AmazonS3Exception ex)
            {
                if (ex.Message.Contains("key does not exist"))
                {
                    throw new NotFoundException($"No profile image for group with id '{groupId}' was found.");
                }
                throw ex;
            }
        }

        public async Task<List<DbEntity_User>> GetMembersOfGroupAsync(int groupId)
        {
            var dbMembers = await _ksuGdcContext.GroupUsers
                                                .Where(g => g.GroupId == groupId)
                                                .Include(ug => ug.User)
                                                .Select(ug => ug.User)
                                                .ToListAsync();
            return dbMembers;
        }

        public async Task<List<DbEntity_Game>> GetGamesOfGroupAsync(int groupId)
        {
            var dbGames = await _ksuGdcContext.Games
                                              .Where(g => g.GroupId == groupId)
                                              .ToListAsync();
            return dbGames;
        }

        #endregion GET

        #region UPDATE

        public async Task<bool> UpdateGroupAsync(DbEntity_Group dbGroup, UpdateDto_Group updateGroup)
        {
            Mapper.Map(updateGroup, dbGroup);
            dbGroup.UpdatedOn = DateTimeOffset.Now;
            _ksuGdcContext.Update(dbGroup);
            await _ksuGdcContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateGroupProfileImageAsync(int groupId, Stream imageStream)
        {
            var transferUtility = new TransferUtility(_s3Client);
            var transferRequest = new TransferUtilityUploadRequest()
            {
                BucketName = AppConfiguration.GetConfig("AWS_S3_BucketName"),
                Key = $"{GroupConfig.DataStoreDirPath}/{groupId}/profile.jpg",
                InputStream = imageStream,
                StorageClass = S3StorageClass.Standard,
                CannedACL = S3CannedACL.PublicRead
            };
            await transferUtility.UploadAsync(transferRequest);
            return true;
        }

        public async Task<bool> AddMemberToGroupAsync(int groupId, int userId)
        {
            var newDbGroupUser = new DbEntity_GroupUser
            {
                GroupId = groupId,
                UserId = userId
            };
            await _ksuGdcContext.GroupUsers.AddAsync(newDbGroupUser);
            await _ksuGdcContext.SaveChangesAsync();
            return true;
        }

        #endregion UPDATE

        #region DELETE

        public async Task<bool> DeleteGroupByIdAsync(int groupId)
        {
            var dbGroup = await GetGroupByIdAsync(groupId);
            _ksuGdcContext.Groups.Remove(dbGroup);
            await _ksuGdcContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveMemberFromGroupAsync(int groupId, int userId)
        {
            var dbGroupUser = await _ksuGdcContext.GroupUsers
                                            .Where(ug => ug.GroupId == groupId && ug.UserId == userId)
                                            .FirstOrDefaultAsync();
            _ksuGdcContext.GroupUsers.Remove(dbGroupUser);
            return true;
        }

        #endregion DELETE
    }
}
