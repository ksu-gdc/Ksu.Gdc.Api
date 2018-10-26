using System;
using System.Linq;
using System.Security.Claims;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AutoMapper;

using Ksu.Gdc.Api.Core.Configurations;
using Ksu.Gdc.Api.Core.Exceptions;
using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Core.Models;
using Ksu.Gdc.Api.Data.DbContexts;
using Ksu.Gdc.Api.Data.Entities;

namespace Ksu.Gdc.Api.Core.Services
{
    public class UtilityService : IUtilityService
    {
        public UtilityService()
        {

        }

        public bool IsPaginationRequest(int pageNumber, int pageSize)
        {
            if (pageNumber == 0)
            {
                if (pageSize != 0)
                {
                    throw new ArgumentException();
                }
                return false;
            }
            else
            {
                if (pageNumber < 0 || pageSize < 0)
                {
                    throw new ArgumentException();
                }
                return true;
            }
        }

        public List<T> Paginate<T>(List<T> collection, int pageNumber, int pageSize)
        {
            return collection
                .GetRange((pageNumber - 1) * pageSize, pageSize);
        }
    }
}
