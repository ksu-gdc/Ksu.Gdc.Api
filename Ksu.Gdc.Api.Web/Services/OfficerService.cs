using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Core.Models;

namespace Ksu.Gdc.Api.Web.Services
{
    public class OfficerService : IOfficerService
    {
        public List<IOfficer<IUser>> GetOfficers()
        {
            return GetOfficersAsync().Result;
        }

        public async Task<List<IOfficer<IUser>>> GetOfficersAsync()
        {

        }

        public List<IOfficer<IUser>> GetOfficersByPosition(string position)
        {
            return GetOfficersByPositionAsync(position).Result;
        }

        public async Task<List<IOfficer<IUser>>> GetOfficersByPositionAsync(string position)
        {

        }
    }
}
