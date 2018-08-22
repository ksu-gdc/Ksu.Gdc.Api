using System;

namespace Ksu.Gdc.Api.Core.Contracts
{
    /// <summary>
    /// User interface.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        string Username { get; set; }
    }
}
