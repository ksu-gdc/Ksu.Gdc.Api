using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.DbContexts;

namespace Ksu.Gdc.Api.Web.Services
{
    public class OfficerService : IOfficerService
    {
        private readonly MemberContext _memberContext;

        public OfficerService(MemberContext memberContext)
        {
            _memberContext = memberContext;
        }

        public List<IOfficer<IUser>> GetOfficers()
        {
            return GetOfficersAsync().Result;
        }

        public async Task<List<IOfficer<IUser>>> GetOfficersAsync()
        {
            throw new NotImplementedException();
        }

        public List<IOfficer<IUser>> GetOfficersByPosition(string position)
        {
            return GetOfficersByPositionAsync(position).Result;
        }

        public async Task<List<IOfficer<IUser>>> GetOfficersByPositionAsync(string position)
        {
            throw new NotImplementedException();
        }
    }
}
