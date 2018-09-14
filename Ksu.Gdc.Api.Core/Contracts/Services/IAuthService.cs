using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;

using Ksu.Gdc.Api.Core.Models;

namespace Ksu.Gdc.Api.Core.Contracts
{
    public interface IAuthService
    {
        /// <summary>
        /// Validates the CAST icket.
        /// </summary>
        /// <returns>The CAST icket.</returns>
        /// <param name="service">Service.</param>
        /// <param name="ticket">Ticket.</param>
        CASValidationResponse ValidateCASTicket(string service, string ticket);

        /// <summary>
        /// Validates the CAST icket async.
        /// </summary>
        /// <returns>The CAST icket async.</returns>
        /// <param name="service">Service.</param>
        /// <param name="ticket">Ticket.</param>
        Task<CASValidationResponse> ValidateCASTicketAsync(string service, string ticket);
    }
}
