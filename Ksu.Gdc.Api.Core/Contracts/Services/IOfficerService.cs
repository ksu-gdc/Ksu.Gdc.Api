using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Ksu.Gdc.Api.Core.Models;

namespace Ksu.Gdc.Api.Core.Contracts
{
    /// <summary>
    /// Officer service interface.
    /// </summary>
    public interface IOfficerService
    {
        /// <summary>
        /// Gets the officers.
        /// </summary>
        /// <returns>The officers.</returns>
        List<OfficerDto> GetOfficers();

        /// <summary>
        /// Gets the officers async.
        /// </summary>
        /// <returns>The officers async.</returns>
        Task<List<OfficerDto>> GetOfficersAsync();

        /// <summary>
        /// Gets the officers by position.
        /// </summary>
        /// <returns>The officers by position.</returns>
        /// <param name="position">Position.</param>
        List<OfficerDto> GetOfficersByPosition(string position);

        /// <summary>
        /// Gets the officers by position async.
        /// </summary>
        /// <returns>The officers by position async.</returns>
        /// <param name="position">Position.</param>
        Task<List<OfficerDto>> GetOfficersByPositionAsync(string position);
    }
}
