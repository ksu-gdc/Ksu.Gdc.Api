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
    public class AuthService : IAuthService
    {
        public CASValidationResponse ValidateCASTicket(string service, string ticket)
        {
            return ValidateCASTicketAsync(service, ticket).Result;
        }

        public async Task<CASValidationResponse> ValidateCASTicketAsync(string service, string ticket)
        {
            using (var client = new HttpClient())
            {
                var url = $"{AppConfiguration.GetConfig("KsuCas_BaseUrl")}/serviceValidate?"
                    + $"service={service}"
                    + $"&ticket={ticket}"
                    + $"&format=JSON";
                var response = new CASValidationResponse(JsonConvert.DeserializeObject(await client.GetStringAsync(url)));
                if (!response.Validated)
                {
                    throw new NotAuthorizedException();
                }
                return response;
            }
        }
    }
}
