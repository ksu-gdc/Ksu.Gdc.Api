using System;
using System.Text;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        public AuthService()
        {

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

        public string BuildToken(AuthDto_User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfiguration.GetConfig("JwtAuth_Key")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };
            foreach (string role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(AppConfiguration.GetConfig("JwtAuth_Issuer"),
                AppConfiguration.GetConfig("JwtAuth_Audience"),
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);
            return "Bearer" + new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
