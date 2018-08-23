using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Contracts;
using Ksu.Gdc.Api.Data.DbContexts;

namespace Ksu.Gdc.Api.Web.Services
{
    public class UserService : IUserService
    {
        private readonly MemberContext _memberContext;

        public UserService(MemberContext memberContext)
        {
            _memberContext = memberContext;
        }

        public IUser GetUserById(int id)
        {
            return GetUserByIdAsync(id).Result;
        }

        public async Task<IUser> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IUser GetUserByUsername(string username)
        {
            return GetUserByUsernameAsync(username).Result;
        }

        public async Task<IUser> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
