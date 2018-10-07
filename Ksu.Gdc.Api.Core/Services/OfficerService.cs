using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;

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
        private readonly IAmazonS3 _s3Client;

        public OfficerService(KsuGdcContext ksuGdcContext, IAmazonS3 s3Client)
        {
            _ksuGdcContext = ksuGdcContext;
            _s3Client = s3Client;
        }

        #region Interface Methods (Synchronous)

        #region GET

        public List<Dto_Officer> GetOfficers()
        {
            return GetOfficersAsync().Result;
        }

        public Dto_Officer GetOfficerById(int officerId)
        {
            return GetOfficerByIdAsync(officerId).Result;
        }

        public List<Dto_Officer> GetOfficersByPosition(string position)
        {
            return GetOfficersByPositionAsync(position).Result;
        }

        #endregion GET

        #region UPDATE

        public bool UpdateOfficerUser(int officerId, int userId)
        {
            return UpdateOfficerUserAsync(officerId, userId).Result;
        }

        #endregion UPDATE

        #region DELETE

        public bool DeleteOfficer(int officerId)
        {
            return DeleteOfficerAsync(officerId).Result;
        }

        public bool DeleteOfficers(string position)
        {
            return DeleteOfficersAsync(position).Result;
        }

        #endregion DELETE

        #endregion Interface Methods (Synchronous)

        #region Interface Methods (Asynchronous)

        #region GET

        public async Task<List<Dto_Officer>> GetOfficersAsync()
        {
            var dbOfficers = await _ksuGdcContext.Officers
                                                 .Include(o => o.User)
                                                 .ToListAsync();
            var dtoOfficers = Mapper.Map<List<Dto_Officer>>(dbOfficers);
            return dtoOfficers;
        }

        public async Task<Dto_Officer> GetOfficerByIdAsync(int officerId)
        {
            var dbOfficer = await _ksuGdcContext.Officers
                                                .Where(o => o.OfficerId == officerId)
                                                .Include(o => o.User)
                                                .FirstOrDefaultAsync();
            if (dbOfficer == null)
            {
                throw new NotFoundException($"No officer with id '{officerId}' was found.");
            }
            var dtoOfficer = Mapper.Map<Dto_Officer>(dbOfficer);
            return dtoOfficer;
        }

        public async Task<List<Dto_Officer>> GetOfficersByPositionAsync(string position)
        {
            var dbOfficers = await _ksuGdcContext.Officers
                                                 .Where(o => o.Position == position)
                                                 .Include(o => o.User)
                                                 .ToListAsync();
            var dtoOfficers = Mapper.Map<List<Dto_Officer>>(dbOfficers);
            return dtoOfficers;
        }

        #endregion GET

        #region UPDATE

        public async Task<bool> UpdateOfficerUserAsync(int officerId, int userId)
        {
            var dbOfficer = await _ksuGdcContext.Officers
                                                .Where(o => o.OfficerId == officerId)
                                                .FirstOrDefaultAsync();
            if (dbOfficer == null)
            {
                throw new NotFoundException($"No officer with id '{officerId}' was found.");
            }
            var dbUser = await _ksuGdcContext.Users
                                             .Where(u => u.UserId == userId)
                                             .FirstOrDefaultAsync();
            if (dbUser == null)
            {
                throw new NotFoundException($"No user with id '{userId}' was found.");
            }
            _ksuGdcContext.Officers.Attach(dbOfficer);
            dbOfficer.UserId = userId;
            await _ksuGdcContext.SaveChangesAsync();
            return true;
        }

        #endregion UPDATE

        #region DELETE

        public async Task<bool> DeleteOfficerAsync(int officerId)
        {
            var dbOfficer = await _ksuGdcContext.Officers
                                                .Where(o => o.OfficerId == officerId)
                                                .FirstOrDefaultAsync();
            if (dbOfficer == null)
            {
                throw new NotFoundException($"No officer with id '{officerId}' was found.");
            }
            _ksuGdcContext.Officers.Remove(dbOfficer);
            await _ksuGdcContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOfficersAsync(string position)
        {
            var dbOfficers = await _ksuGdcContext.Officers
                                                 .Where(o => o.Position == position)
                                                 .ToListAsync();
            _ksuGdcContext.Officers.RemoveRange(dbOfficers);
            await _ksuGdcContext.SaveChangesAsync();
            return true;
        }

        #endregion DELETE

        #endregion Interface Methods (Asynchronous)
    }
}
