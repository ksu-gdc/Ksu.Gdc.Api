using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
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

        public OfficerService(KsuGdcContext ksuGdcContext)
        {
            _ksuGdcContext = ksuGdcContext;
        }

        public List<Dto_Officer> GetOfficers()
        {
            return GetOfficersAsync().Result;
        }

        public async Task<List<Dto_Officer>> GetOfficersAsync()
        {
            var dbOfficers = await _ksuGdcContext.Officers.Include(o => o.User).ToListAsync();
            if (dbOfficers.Count == 0)
            {
                throw new NotFoundException("No officers were found.");
            }
            var dtoOfficers = Mapper.Map<List<Dto_Officer>>(dbOfficers);
            return dtoOfficers;
        }

        public List<Dto_Officer> GetOfficersByPosition(string position)
        {
            return GetOfficersByPositionAsync(position).Result;
        }

        public async Task<List<Dto_Officer>> GetOfficersByPositionAsync(string position)
        {
            var dbOfficers = await _ksuGdcContext.Officers.Include(o => o.User).Where(o => o.Position == position).ToListAsync();
            if (dbOfficers.Count == 0)
            {
                throw new NotFoundException($"No officers for position '{position}' were found.");
            }
            var dtoOfficers = Mapper.Map<List<Dto_Officer>>(dbOfficers);
            return dtoOfficers;
        }
    }
}
