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

namespace Ksu.Gdc.Api.Web.Services
{
    public class UserService : IUserService
    {
        private readonly MemberContext _memberContext;

        public UserService(MemberContext memberContext)
        {
            _memberContext = memberContext;
        }

        public UserDto GetUserById(int id)
        {
            return GetUserByIdAsync(id).Result;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var dbUser = await _memberContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (dbUser == null)
            {
                throw new NotFoundException($"No user with Id '{id}' was found.");
            }
            var userDto = Mapper.Map<UserDto>(dbUser);
            return userDto;
        }

        public UserDto GetUserByUsername(string username)
        {
            return GetUserByUsernameAsync(username).Result;
        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            var dbUser = await _memberContext.Users.Where(u => u.Username == username).FirstOrDefaultAsync<IUser>();
            if (dbUser == null)
            {
                throw new NotFoundException($"No user with username '{username}' was found.");
            }
            var userDto = Mapper.Map<UserDto>(dbUser);
            return userDto;
        }
    }
}
