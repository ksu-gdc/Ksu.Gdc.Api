﻿using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        /// <param name="userId">User identifier.</param>
        Dto_User GetUserById(int userId);

        /// <summary>
        /// Gets the user by identifier async.
        /// </summary>
        /// <returns>The user by identifier async.</returns>
        /// <param name="userId">User identifier.</param>
        Task<Dto_User> GetUserByIdAsync(int userId);

        /// <summary>
        /// Gets the user by username.
        /// </summary>
        /// <returns>The user by username.</returns>
        /// <param name="username">Username.</param>
        Dto_User GetUserByUsername(string username);

        /// <summary>
        /// Gets the user by username async.
        /// </summary>
        /// <returns>The user by username async.</returns>
        /// <param name="username">Username.</param>
        Task<Dto_User> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name="newUser">New user.</param>
        Dto_User AddUser(CreateDto_User newUser);

        /// <summary>
        /// Adds the user async.
        /// </summary>
        /// <returns>The user async.</returns>
        /// <param name="newUser">New user.</param>
        Task<Dto_User> AddUserAsync(CreateDto_User newUser);

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <returns><c>true</c>, if user was updated, <c>false</c> otherwise.</returns>
        /// <param name="userId">User identifier.</param>
        /// <param name="user">User.</param>
        bool UpdateUser(int userId, UpdateDto_User user);

        /// <summary>
        /// Updates the user async.
        /// </summary>
        /// <returns>The user async.</returns>
        /// <param name="userId">User identifier.</param>
        /// <param name="user">User.</param>
        Task<bool> UpdateUserAsync(int userId, UpdateDto_User user);

        /// <summary>
        /// Updates the user profile image.
        /// </summary>
        /// <returns><c>true</c>, if user profile image was updated, <c>false</c> otherwise.</returns>
        /// <param name="userId">User identifier.</param>
        /// <param name="imageStream">Image stream.</param>
        bool UpdateUserProfileImage(int userId, Stream imageStream);

        /// <summary>
        /// Updates the user profile image async.
        /// </summary>
        /// <returns>The user profile image async.</returns>
        /// <param name="userId">User identifier.</param>
        /// <param name="imageStream">Image stream.</param>
        Task<bool> UpdateUserProfileImageAsync(int userId, Stream imageStream);

        /// <summary>
        /// Gets the user profile image.
        /// </summary>
        /// <returns>The user profile image.</returns>
        /// <param name="userId">User identifier.</param>
        Stream GetUserProfileImage(int userId);

        /// <summary>
        /// Gets the user profile image async.
        /// </summary>
        /// <returns>The user profile image async.</returns>
        /// <param name="userId">User identifier.</param>
        Task<Stream> GetUserProfileImageAsync(int userId);
    }
}
