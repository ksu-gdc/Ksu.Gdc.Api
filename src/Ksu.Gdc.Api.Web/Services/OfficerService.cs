using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Amazon.S3;
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
        private readonly IAmazonS3 _s3Client;

        public OfficerService(KsuGdcContext ksuGdcContext, IAmazonS3 s3Client)
        {
            _ksuGdcContext = ksuGdcContext;
            _s3Client = s3Client;
        }

        #region CREATE

        public async Task<DbEntity_Officer> CreateOfficerAsync(CreateDto_Officer newOfficer)
        {
            var newDbOfficer = Mapper.Map<DbEntity_Officer>(newOfficer);
            await _ksuGdcContext.Officers.AddAsync(newDbOfficer);
            await _ksuGdcContext.SaveChangesAsync();
            return newDbOfficer;
        }

        #endregion CREATE

        #region GET

        public async Task<List<DbEntity_Officer>> GetOfficersAsync()
        {
            var dbOfficers = await _ksuGdcContext.Officers
                                                 .Include(o => o.User)
                                                 .ToListAsync();
            return dbOfficers;
        }

        public async Task<DbEntity_Officer> GetOfficerByIdAsync(int officerId)
        {
            var dbOfficer = await _ksuGdcContext.Officers
                                                .Where(o => o.OfficerId == officerId)
                                                .Include(o => o.User)
                                                .FirstOrDefaultAsync();
            if (dbOfficer == null)
            {
                throw new NotFoundException($"No officer with id '{officerId}' was found.");
            }
            return dbOfficer;
        }

        public async Task<List<DbEntity_Officer>> GetOfficersByPositionAsync(string position)
        {
            var dbOfficers = await _ksuGdcContext.Officers
                                                 .Where(o => o.Position == position)
                                                 .Include(o => o.User)
                                                 .ToListAsync();
            return dbOfficers;
        }

        public async Task<List<DbEntity_Officer>> GetOfficersByUserIdAsync(int userId)
        {
            var dbOfficers = await _ksuGdcContext.Officers
                                                 .Where(o => o.UserId == userId)
                                                 .ToListAsync();
            return dbOfficers;
        }

        #endregion GET

        #region UPDATE

        public async Task<bool> UpdateOfficerAsync(DbEntity_Officer dbOfficer, UpdateDto_Officer updateOfficer)
        {
            Mapper.Map(updateOfficer, dbOfficer);
            dbOfficer.UpdatedOn = DateTimeOffset.Now;
            _ksuGdcContext.Update(dbOfficer);
            await _ksuGdcContext.SaveChangesAsync();
            return true;
        }

        #endregion UPDATE

        #region DELETE

        public async Task<bool> DeleteOfficerByIdAsync(int officerId)
        {
            var dbOfficer = await GetOfficerByIdAsync(officerId);
            _ksuGdcContext.Officers.Remove(dbOfficer);
            await _ksuGdcContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOfficersByPositionAsync(string position)
        {
            var dbOfficers = await GetOfficersByPositionAsync(position);
            _ksuGdcContext.Officers.RemoveRange(dbOfficers);
            await _ksuGdcContext.SaveChangesAsync();
            return true;
        }

        #endregion DELETE
    }
}
