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

        public Dto_User GetUserById(int userId)
        {
            return GetUserByIdAsync(userId).Result;
        }

        public async Task<Dto_User> GetUserByIdAsync(int userId)
        {
            var dbUser = await _ksuGdcContext.Users
                                             .Where(u => u.UserId == userId)
                                             .FirstOrDefaultAsync();
            if (dbUser == null)
            {
                throw new NotFoundException($"No user with id '{userId}' was found.");
            }
            var dtoUser = Mapper.Map<Dto_User>(dbUser);
            return dtoUser;
        }

        public Dto_User GetUserByUsername(string username)
        {
            return GetUserByUsernameAsync(username).Result;
        }

        public async Task<Dto_User> GetUserByUsernameAsync(string username)
        {
            var dbUser = await _ksuGdcContext.Users
                                             .Where(u => u.Username == username)
                                             .FirstOrDefaultAsync();
            if (dbUser == null)
            {
                throw new NotFoundException($"No user with username '{username}' was found.");
            }
            var dtoUser = Mapper.Map<Dto_User>(dbUser);
            return dtoUser;
        }

        public Dto_User AddUser(CreateDto_User newUser)
        {
            return AddUserAsync(newUser).Result;
        }

        public async Task<Dto_User> AddUserAsync(CreateDto_User newUser)
        {
            var newDbUser = Mapper.Map<ModelEntity_User>(newUser);
            await _ksuGdcContext.Users.AddAsync(newDbUser);
            await _ksuGdcContext.SaveChangesAsync();
            return Mapper.Map<Dto_User>(newDbUser);
        }

        public bool UpdateUser(int userId, UpdateDto_User user)
        {
            return UpdateUserAsync(userId, user).Result;
        }

        public async Task<bool> UpdateUserAsync(int userId, UpdateDto_User user)
        {
            var dbUser = await _ksuGdcContext.Users
                                             .Where(u => u.UserId == userId)
                                             .FirstOrDefaultAsync();
            if (dbUser == null)
            {
                throw new NotFoundException($"No user with id '{userId}' was found.");
            }
            _ksuGdcContext.Users.Attach(dbUser);
            _ksuGdcContext.Entry(dbUser).CurrentValues.SetValues(user);
            await _ksuGdcContext.SaveChangesAsync();
            return true;
        }

        public bool UpdateUserProfileImage(int userId, Stream imageStream)
        {
            return UpdateUserProfileImageAsync(userId, imageStream).Result;
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

        public Stream GetUserProfileImage(int userId)
        {
            return GetUserProfileImageAsync(userId).Result;
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

        public List<Dto_Group> GetGroupsOfUser(int userId)
        {
            return GetGroupsOfUserAsync(userId).Result;
        }

        public async Task<List<Dto_Group>> GetGroupsOfUserAsync(int userId)
        {
            var dbGroups = await _ksuGdcContext.User_Group
                                               .Include(ug => ug.Group)
                                               .Where(ug => ug.UserId == userId)
                                               .Select(ug => ug.Group)
                                               .ToListAsync();
            var dtoGroups = Mapper.Map<List<Dto_Group>>(dbGroups);
            return dtoGroups;
        }

        public List<Dto_Game> GetGamesOfUser(int userId)
        {
            return GetGamesOfUserAsync(userId).Result;
        }

        public async Task<List<Dto_Game>> GetGamesOfUserAsync(int userId)
        {
            var dbGames = await _ksuGdcContext.Games.Where(g => g.UserId == userId)
                                      .ToListAsync();
            var dtoGames = Mapper.Map<List<Dto_Game>>(dbGames);
            return dtoGames;
        }
    }
}
