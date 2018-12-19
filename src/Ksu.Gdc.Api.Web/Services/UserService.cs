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

        public async Task<DbEntity_User> CreateUserAsync(CreateDto_User newUser)
        {
            var newDbUser = Mapper.Map<DbEntity_User>(newUser);
            await _ksuGdcContext.Users.AddAsync(newDbUser);
            await _ksuGdcContext.SaveChangesAsync();
            return newDbUser;
        }

        #endregion CREATE

        #region GET

        public async Task<List<DbEntity_User>> GetUsersAsync()
        {
            var dbUsers = await _ksuGdcContext.Users
                                            .ToListAsync();
            return dbUsers;
        }

        public async Task<DbEntity_User> GetUserByIdAsync(int userId)
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

        public async Task<DbEntity_User> GetUserByUsernameAsync(string username)
        {
            var user = await _ksuGdcContext.Users
                                             .Where(u => u.Username == username)
                                             .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new NotFoundException($"No user with username '{username}' was found.");
            }
            return user;
        }

        public async Task<Stream> GetUserProfileImageAsync(int userId)
        {
            try
            {
                var transferUtility = new TransferUtility(_s3Client);
                var transferRequest = new TransferUtilityOpenStreamRequest()
                {
                    BucketName = AppConfiguration.GetConfig("AWS_S3_BucketName"),
                    Key = $"{UserConfig.DataStoreDirPath}/{userId}/profile.jpg"
                };
                var stream = await transferUtility.OpenStreamAsync(transferRequest);
                return stream;
            }
            catch (AmazonS3Exception ex)
            {
                if (ex.Message.Contains("key does not exist"))
                {
                    throw new NotFoundException($"No profile image for user with id '{userId}' was found.");
                }
                throw ex;
            }
        }

        public async Task<List<DbEntity_Game>> GetGamesOfUserAsync(int userId)
        {
            var games = await _ksuGdcContext.UserGames
                .Where(gu => gu.UserId == userId)
                .Include(gu => gu.Game)
                .Select(gu => gu.Game)
                .ToListAsync();
            return games;
        }

        #endregion GET

        #region UPDATE

        public async Task<bool> UpdateUserAsync(DbEntity_User dbUser, UpdateDto_User updateUser)
        {
            Mapper.Map(updateUser, dbUser);
            dbUser.UpdatedOn = DateTimeOffset.Now;
            _ksuGdcContext.Update(dbUser);
            await _ksuGdcContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserProfileImageAsync(int userId, Stream imageStream)
        {
            var transferUtility = new TransferUtility(_s3Client);
            var transferRequest = new TransferUtilityUploadRequest()
            {
                BucketName = AppConfiguration.GetConfig("AWS_S3_BucketName"),
                Key = $"{UserConfig.DataStoreDirPath}/{userId}/profile.jpg",
                InputStream = imageStream,
                StorageClass = S3StorageClass.Standard,
                CannedACL = S3CannedACL.PublicRead
            };
            await transferUtility.UploadAsync(transferRequest);
            return true;
        }

        public async Task<bool> AddGameToUser(int userId, int gameId)
        {
            var userGame = new DbEntity_UserGame()
            {
                UserId = userId,
                GameId = gameId
            };
            await _ksuGdcContext.UserGames.AddAsync(userGame);
            await _ksuGdcContext.SaveChangesAsync();
            return true;
        }

        #endregion UPDATE

        #region DELETE

        public async Task<bool> RemoveGameFromUser(int userId, int gameId)
        {
            var dbUserGame = await _ksuGdcContext.UserGames
                .Where(ug => ug.UserId == userId && ug.GameId == gameId)
                .FirstOrDefaultAsync();
            _ksuGdcContext.UserGames.Remove(dbUserGame);
            return true;
        }

        #endregion DELETE
    }
}
