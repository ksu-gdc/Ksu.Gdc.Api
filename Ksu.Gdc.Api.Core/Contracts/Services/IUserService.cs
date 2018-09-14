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
        UserDto GetUserById(int id);

        /// <summary>
        /// Gets the user by identifier async.
        /// </summary>
        /// <returns>The user by identifier async.</returns>
        /// <param name="id">Identifier.</param>
        Task<UserDto> GetUserByIdAsync(int id);

        /// <summary>
        /// Gets the user by username.
        /// </summary>
        /// <returns>The user by username.</returns>
        /// <param name="username">Username.</param>
        UserDto GetUserByUsername(string username);

        /// <summary>
        /// Gets the user by username async.
        /// </summary>
        /// <returns>The user by username async.</returns>
        /// <param name="username">Username.</param>
        Task<UserDto> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name="newUser">New user.</param>
        UserDto AddUser(UserForCreationDto newUser);

        /// <summary>
        /// Adds the user async.
        /// </summary>
        /// <returns>The user async.</returns>
        /// <param name="newUser">New user.</param>
        Task<UserDto> AddUserAsync(UserForCreationDto newUser);

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <returns><c>true</c>, if user was updated, <c>false</c> otherwise.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="user">User.</param>
        bool UpdateUser(int id, UserForUpdateDto user);

        /// <summary>
        /// Updates the user async.
        /// </summary>
        /// <returns>The user async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="user">User.</param>
        Task<bool> UpdateUserAsync(int id, UserForUpdateDto user);
    }
}
