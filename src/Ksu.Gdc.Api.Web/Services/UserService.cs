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
    public class UserService : IUserService
    {
        private readonly KsuGdcContext _ksuGdcContext;
        private readonly IAmazonS3 _s3Client;

        public UserService(KsuGdcContext ksuGdcContext, IAmazonS3 s3Client)
        {
            _ksuGdcContext = ksuGdcContext;
            _s3Client = s3Client;
        }

        #region CREATE

        public async Task<bool> CreateAsync(DbEntity_User createdUser)
        {
            await _ksuGdcContext.Users.AddAsync(createdUser);
            return true;
        }
        public async Task<DbEntity_User> CreateAsync(CreateDto_User newUser)
        {
            var createdUser = Mapper.Map<DbEntity_User>(newUser);
            var success = await CreateAsync(createdUser);
            return createdUser;
        }

        #endregion CREATE

        #region GET

        public async Task<List<DbEntity_User>> GetAllAsync()
        {
            var users = await _ksuGdcContext.Users
                .ToListAsync();
            return users;
        }

        public async Task<DbEntity_User> GetByIdAsync(int userId)
        {
            var user = await _ksuGdcContext.Users
                .Where(u => u.UserId == userId)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new NotFoundException($"No user with id '{userId}' was found.");
            }
            return user;
        }

        public async Task<Stream> GetImageAsync(DbEntity_User user)
        {
            try
            {
                var transferUtility = new TransferUtility(_s3Client);
                var transferRequest = new TransferUtilityOpenStreamRequest()
                {
                    BucketName = AppConfiguration.GetConfig("AWS_S3_BucketName"),
                    Key = $"{UserConfig.DataStoreDirPath}/{user.UserId}/profile.jpg"
                };
                var stream = await transferUtility.OpenStreamAsync(transferRequest);
                return stream;
            }
            catch (AmazonS3Exception ex)
            {
                if (ex.Message.Contains("key does not exist"))
                {
                    throw new NotFoundException($"No profile image for user with id '{user.UserId}' was found.");
                }
                throw ex;
            }
        }
        public async Task<Stream> GetImageAsync(int userId)
        {
            var user = await GetByIdAsync(userId);
            var stream = await GetImageAsync(user);
            return stream;
        }

        public async Task<List<DbEntity_Game>> GetGamesAsync(DbEntity_User user)
        {
            var games = await _ksuGdcContext.GameUsers
                .Where(gu => gu.UserId == user.UserId)
                .Include(gu => gu.Game)
                .Select(gu => gu.Game)
                .ToListAsync();
            return games;
        }
        public async Task<List<DbEntity_Game>> GetGamesAsync(int userId)
        {
            var user = await GetByIdAsync(userId);
            var games = await GetGamesAsync(user);
            return games;
        }

        #endregion GET

        #region UPDATE

        public async Task<bool> UpdateAsync(DbEntity_User updatedUser)
        {
            updatedUser.UpdatedOn = DateTimeOffset.Now;
            _ksuGdcContext.Update(updatedUser);
            return true;
        }

        public async Task<bool> UpdateImageAsync(DbEntity_User user, Stream image)
        {
            var transferUtility = new TransferUtility(_s3Client);
            var transferRequest = new TransferUtilityUploadRequest()
            {
                BucketName = AppConfiguration.GetConfig("AWS_S3_BucketName"),
                Key = $"{UserConfig.DataStoreDirPath}/{user.UserId}/profile.jpg",
                InputStream = image,
                StorageClass = S3StorageClass.Standard,
                CannedACL = S3CannedACL.PublicRead
            };
            await transferUtility.UploadAsync(transferRequest);
            return true;
        }
        public async Task<bool> UpdateImageAsync(int userId, Stream image)
        {
            var user = await GetByIdAsync(userId);
            var success = await UpdateImageAsync(user, image);
            return success;
        }

        #endregion UPDATE

        #region DELETE

        #endregion DELETE

        public async Task<int> SaveChangesAsync()
        {
            return await _ksuGdcContext.SaveChangesAsync();
        }
    }
}
