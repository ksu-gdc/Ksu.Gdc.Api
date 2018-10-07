using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Models;

namespace Ksu.Gdc.Api.Core.Contracts
{
    /// <summary>
    /// Users service interface.
    /// </summary>
    public interface IUserService
    {
        #region Interface Methods (Synchronous)

        #region GET

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <returns>The user by identifier.</returns>
        /// <param name="userId">Users identifier.</param>
        Dto_User GetUserById(int userId);

        /// <summary>
        /// Gets the user by username.
        /// </summary>
        /// <returns>The user by username.</returns>
        /// <param name="username">Username.</param>
        Dto_User GetUserByUsername(string username);

        /// <summary>
        /// Gets the user profile image.
        /// </summary>
        /// <returns>The user profile image.</returns>
        /// <param name="userId">Users identifier.</param>
        Stream GetUserProfileImage(int userId);

        /// <summary>
        /// Gets the groups of user.
        /// </summary>
        /// <returns>The groups of user.</returns>
        /// <param name="userId">Users identifier.</param>
        List<Dto_Group> GetGroupsOfUser(int userId);

        /// <summary>
        /// Gets the games of user.
        /// </summary>
        /// <returns>The games of user.</returns>
        /// <param name="userId">Users identifier.</param>
        List<Dto_Game> GetGamesOfUser(int userId);

        #endregion GET

        #region ADD

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name="newUser">New user.</param>
        Dto_User AddUser(CreateDto_User newUser);

        #endregion ADD

        #region UPDATE

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <returns><c>true</c>, if user was updated, <c>false</c> otherwise.</returns>
        /// <param name="userId">Users identifier.</param>
        /// <param name="user">Users.</param>
        bool UpdateUser(int userId, UpdateDto_User user);

        /// <summary>
        /// Updates the user profile image.
        /// </summary>
        /// <returns><c>true</c>, if user profile image was updated, <c>false</c> otherwise.</returns>
        /// <param name="userId">Users identifier.</param>
        /// <param name="imageStream">Image stream.</param>
        bool UpdateUserProfileImage(int userId, Stream imageStream);

        #endregion UPDATE

        #endregion Interface Methods (Synchronous)

        #region Interface Methods (Asynchronous)

        #region GET

        /// <summary>
        /// Gets the user by identifier async.
        /// </summary>
        /// <returns>The user by identifier async.</returns>
        /// <param name="userId">Users identifier.</param>
        Task<Dto_User> GetUserByIdAsync(int userId);

        /// <summary>
        /// Gets the user by username async.
        /// </summary>
        /// <returns>The user by username async.</returns>
        /// <param name="username">Username.</param>
        Task<Dto_User> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Gets the user profile image async.
        /// </summary>
        /// <returns>The user profile image async.</returns>
        /// <param name="userId">Users identifier.</param>
        Task<Stream> GetUserProfileImageAsync(int userId);

        /// <summary>
        /// Gets the groups of user async.
        /// </summary>
        /// <returns>The groups of user async.</returns>
        /// <param name="userId">Users identifier.</param>
        Task<List<Dto_Group>> GetGroupsOfUserAsync(int userId);

        /// <summary>
        /// Gets the games of user async.
        /// </summary>
        /// <returns>The games of user async.</returns>
        /// <param name="userId">Users identifier.</param>
        Task<List<Dto_Game>> GetGamesOfUserAsync(int userId);

        #endregion GET

        #region ADD

        /// <summary>
        /// Adds the user async.
        /// </summary>
        /// <returns>The user async.</returns>
        /// <param name="newUser">New user.</param>
        Task<Dto_User> AddUserAsync(CreateDto_User newUser);

        #endregion ADD

        #region UPDATE

        /// <summary>
        /// Updates the user async.
        /// </summary>
        /// <returns>The user async.</returns>
        /// <param name="userId">Users identifier.</param>
        /// <param name="user">Users.</param>
        Task<bool> UpdateUserAsync(int userId, UpdateDto_User user);

        /// <summary>
        /// Updates the user profile image async.
        /// </summary>
        /// <returns>The user profile image async.</returns>
        /// <param name="userId">Users identifier.</param>
        /// <param name="imageStream">Image stream.</param>
        Task<bool> UpdateUserProfileImageAsync(int userId, Stream imageStream);

        #endregion UPDATE

        #endregion Interface Methods (Asynchronous)
    }
}
