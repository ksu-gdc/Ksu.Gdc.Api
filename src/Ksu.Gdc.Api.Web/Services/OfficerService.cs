using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.DbContexts;
using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Core.Services
{
    public class OfficerService : IOfficerService
    {
        private readonly KsuGdcContext _ksuGdcContext;
        private readonly IUserService _userService;

        public OfficerService(KsuGdcContext ksuGdcContext, IUserService userService)
        {
            _ksuGdcContext = ksuGdcContext;
            _userService = userService;
        }

        #region CREATE

        public async Task<bool> CreateAsync(DbEntity_Officer createdOfficer)
        {
            await _ksuGdcContext.Officers.AddAsync(createdOfficer);
            return true;
        }

        #endregion CREATE

        #region GET

        public async Task<List<DbEntity_Officer>> GetAllAsync()
        {
            var officers = await _ksuGdcContext.Officers
                .Include(o => o.User)
                .ToListAsync();
            return officers;
        }

        public async Task<DbEntity_Officer> GetByIdAsync(int officerId)
        {
            var officer = await _ksuGdcContext.Officers
                .Where(o => o.OfficerId == officerId)
                .Include(o => o.User)
                .FirstOrDefaultAsync();
            if (officer == null)
            {
                throw new NotFoundException($"No officer with id '{officerId}' was found.");
            }
            return officer;
        }

        public async Task<List<DbEntity_Officer>> GetByPositionAsync(string position)
        {
            var officers = await _ksuGdcContext.Officers
                .Where(o => o.Position == position)
                .Include(o => o.User)
                .ToListAsync();
            return officers;
        }

        public async Task<List<DbEntity_Officer>> GetByUserAsync(DbEntity_User user)
        {
            var officers = await _ksuGdcContext.Officers
                .Where(o => o.UserId == user.UserId)
                .ToListAsync();
            return officers;
        }
        public async Task<List<DbEntity_Officer>> GetByUserAsync(int userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            var officers = await GetByUserAsync(user);
            return officers;
        }

        #endregion GET

        #region UPDATE

        public async Task<bool> UpdateAsync(DbEntity_Officer updatedOfficer)
        {
            _ksuGdcContext.Update(updatedOfficer);
            return true;
        }

        #endregion UPDATE

        #region DELETE

        public async Task<bool> DeleteAsync(DbEntity_Officer officer)
        {
            _ksuGdcContext.Officers.Remove(officer);
            return true;
        }
        public async Task<bool> DeleteAsync(List<DbEntity_Officer> officers)
        {
            _ksuGdcContext.Officers.RemoveRange(officers);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(int officerId)
        {
            var officer = await GetByIdAsync(officerId);
            var success = await DeleteAsync(officer);
            return success;
        }

        public async Task<bool> DeleteByPositionAsync(string position)
        {
            var officers = await GetByPositionAsync(position);
            var success = await DeleteAsync(officers);
            return success;
        }

        #endregion DELETE

        public async Task<int> SaveChangesAsync()
        {
            return await _ksuGdcContext.SaveChangesAsync();
        }
    }
}
