using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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

        public UserService(KsuGdcContext ksuGdcContext)
        {
            _ksuGdcContext = ksuGdcContext;
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

        public async Task<DbEntity_Image> GetImageAsync(DbEntity_User user)
        {
            var image = await _ksuGdcContext.UserImages
                .Where(ui => ui.UserId == user.UserId)
                .Select(ui => ui.Image)
                .FirstOrDefaultAsync();
            if (image == null)
            {
                throw new NotFoundException($"No image with user id '{user.UserId}' was found.");
            }
            return image;
        }
        public async Task<DbEntity_Image> GetImageAsync(int userId)
        {
            var user = await GetByIdAsync(userId);
            var image = await GetImageAsync(user);
            return image;
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
            _ksuGdcContext.Update(updatedUser);
            return true;
        }

        public async Task<bool> UpdateImageAsync(int userId, UpdateDto_Image imageUpdate)
        {
            var user = await GetByIdAsync(userId);
            DbEntity_Image image;
            try
            {
                image = await GetImageAsync(user);
            }
            catch (NotFoundException)
            {
                var newImage = Mapper.Map<DbEntity_Image>(imageUpdate);
                await _ksuGdcContext.Images.AddAsync(newImage);
                var userImage = new DbEntity_UserImage()
                {
                    UserId = user.UserId,
                    ImageId = newImage.ImageId
                };
                await _ksuGdcContext.UserImages.AddAsync(userImage);
                return true;
            }
            Mapper.Map(imageUpdate, image);
            _ksuGdcContext.Images.Update(image);
            return true;
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
