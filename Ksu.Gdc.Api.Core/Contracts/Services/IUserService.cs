﻿using System;
using System.Threading.Tasks;

using Ksu.Gdc.Api.Core.Models;

namespace Ksu.Gdc.Api.Core.Contracts
{
    /// <summary>
    /// User service interface.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <returns>The user by identifier.</returns>
        /// <param name="id">Identifier.</param>
        User GetUserById(int id);

        /// <summary>
        /// Gets the user by identifier async.
        /// </summary>
        /// <returns>The user by identifier async.</returns>
        /// <param name="id">Identifier.</param>
        Task<User> GetUserByIdAsync(int id);

        /// <summary>
        /// Gets the user by username.
        /// </summary>
        /// <returns>The user by username.</returns>
        /// <param name="username">Username.</param>
        User GetUserByUsername(string username);

        /// <summary>
        /// Gets the user by username async.
        /// </summary>
        /// <returns>The user by username async.</returns>
        /// <param name="username">Username.</param>
        Task<User> GetUserByUsernameAsync(string username);
    }
}
