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
    public class UserService : IUserService
    {
        private readonly KsuGdcContext _ksuGdcContext;

        public UserService(KsuGdcContext ksuGdcContext)
        {
            _ksuGdcContext = ksuGdcContext;
        }

        public UserDto GetUserById(int id)
        {
            return GetUserByIdAsync(id).Result;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var dbUser = await _ksuGdcContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
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
            var dbUser = await _ksuGdcContext.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
            if (dbUser == null)
            {
                throw new NotFoundException($"No user with username '{username}' was found.");
            }
            var userDto = Mapper.Map<UserDto>(dbUser);
            return userDto;
        }

        public UserDto AddUser(UserForCreationDto newUser)
        {
            return AddUserAsync(newUser).Result;
        }

        public async Task<UserDto> AddUserAsync(UserForCreationDto newUser)
        {
            var newDbUser = Mapper.Map<UserDbEntity>(newUser);
            await _ksuGdcContext.Users.AddAsync(newDbUser);
            await _ksuGdcContext.SaveChangesAsync();
            return Mapper.Map<UserDto>(newDbUser);
        }

        public bool UpdateUser(int id, UserForUpdateDto user)
        {
            return UpdateUserAsync(id, user).Result;
        }

        public async Task<bool> UpdateUserAsync(int id, UserForUpdateDto user)
        {
            var dbUser = await _ksuGdcContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (dbUser == null)
            {
                throw new NotFoundException($"No user with id '{id}' was found.");
            }
            _ksuGdcContext.Users.Attach(dbUser);
            _ksuGdcContext.Entry(dbUser).CurrentValues.SetValues(user);
            await _ksuGdcContext.SaveChangesAsync();
            return true;
        }
    }
}
