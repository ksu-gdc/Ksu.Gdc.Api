using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Models;

namespace Ksu.Gdc.Api.Core.Contracts
{
    /// <summary>
    /// Officers service interface.
    /// </summary>
    public interface IOfficerService
    {
        #region Interface Methods (Synchronous)

        #region GET

        /// <summary>
        /// Gets the officers.
        /// </summary>
        /// <returns>The officers.</returns>
        List<Dto_Officer> GetOfficers();

        /// <summary>
        /// Gets the officer by identifier.
        /// </summary>
        /// <returns>The officer by identifier.</returns>
        /// <param name="officerId">Officers identifier.</param>
        Dto_Officer GetOfficerById(int officerId);

        /// <summary>
        /// Gets the officers by position.
        /// </summary>
        /// <returns>The officers by position.</returns>
        /// <param name="position">Position.</param>
        List<Dto_Officer> GetOfficersByPosition(string position);

        #endregion GET

        #region UPDATE

        /// <summary>
        /// Updates the officer user.
        /// </summary>
        /// <returns><c>true</c>, if officer user was updated, <c>false</c> otherwise.</returns>
        /// <param name="userId">Users identifier.</param>
        bool UpdateOfficerUser(int officerId, int userId);

        #endregion UPDATE

        #region DELETE

        /// <summary>
        /// Deletes the officer.
        /// </summary>
        /// <returns><c>true</c>, if officer was deleted, <c>false</c> otherwise.</returns>
        /// <param name="officerId">Officers identifier.</param>
        bool DeleteOfficer(int officerId);

        /// <summary>
        /// Deletes the officers.
        /// </summary>
        /// <returns><c>true</c>, if officers was deleted, <c>false</c> otherwise.</returns>
        /// <param name="position">Position.</param>
        bool DeleteOfficers(string position);

        #endregion DELETE

        #endregion Interface Methods (Synchronous)

        #region Interface Methods (Asynchronous)

        #region GET

        /// <summary>
        /// Gets the officers async.
        /// </summary>
        /// <returns>The officers async.</returns>
        Task<List<Dto_Officer>> GetOfficersAsync();

        /// <summary>
        /// Gets the officer by identifier async.
        /// </summary>
        /// <returns>The officer by identifier async.</returns>
        /// <param name="officerId">Officers identifier.</param>
        Task<Dto_Officer> GetOfficerByIdAsync(int officerId);

        /// <summary>
        /// Gets the officers by position async.
        /// </summary>
        /// <returns>The officers by position async.</returns>
        /// <param name="position">Position.</param>
        Task<List<Dto_Officer>> GetOfficersByPositionAsync(string position);

        #endregion GET

        #region UPDATE

        /// <summary>
        /// Updates the officer user async.
        /// </summary>
        /// <returns>The officer user async.</returns>
        /// <param name="userId">Users identifier.</param>
        Task<bool> UpdateOfficerUserAsync(int officerId, int userId);

        #endregion UPDATE

        #region DELETE

        /// <summary>
        /// Deletes the officer async.
        /// </summary>
        /// <returns>The officer async.</returns>
        /// <param name="officerId">Officers identifier.</param>
        Task<bool> DeleteOfficerAsync(int officerId);

        /// <summary>
        /// Deletes the officers async.
        /// </summary>
        /// <returns>The officers async.</returns>
        /// <param name="position">Position.</param>
        Task<bool> DeleteOfficersAsync(string position);

        #endregion DELETE

        #endregion Interface Methods (Synchronous)
    }
}
