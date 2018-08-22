using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Contracts;

namespace Ksu.Gdc.Api.Web.Services
{
    public class UserService : IUserService
    {
        public IUser GetUserById(int id)
        {
            return GetUserByIdAsync(id).Result;
        }

        public async Task<IUser> GetUserByIdAsync(int id)
        {

        }

        public IUser GetUserByUsername(string username)
        {
            return GetUserByUsernameAsync(username).Result;
        }

        public async Task<IUser> GetUserByUsernameAsync(string username)
        {

        }
    }
}
