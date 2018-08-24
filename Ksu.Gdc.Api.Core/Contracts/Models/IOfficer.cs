using System;

namespace Ksu.Gdc.Api.Core.Contracts
{
    /// <summary>
    /// Officer interface.
    /// </summary>
    public interface IOfficer<TUser>
    {
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        string Position { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        TUser User { get; set; }
    }
}
