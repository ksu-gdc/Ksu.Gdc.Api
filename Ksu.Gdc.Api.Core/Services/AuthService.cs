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
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public UserDto ValidateCASTicket(string service, string ticket)
        {
            return ValidateCASTicketAsync(service, ticket).Result;
        }

        public async Task<UserDto> ValidateCASTicketAsync(string service, string ticket)
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
                try
                {
                    var id = response.ServiceResponse.AuthenticationSuccess.Attributes.KsuPersonWildcatId[0];
                    var userDto = await _userService.GetUserByIdAsync(id);
                    return userDto;
                }
                catch (NotFoundException)
                {
                    var newUser = new UserForCreationDto(response.ServiceResponse.AuthenticationSuccess.Attributes);
                    var userDto = await _userService.AddUserAsync(newUser);
                    return userDto;
                }
            }
        }
    }
}
