using System.Threading.Tasks;

using Ksu.Gdc.Api.Core.Models;

namespace Ksu.Gdc.Api.Core.Contracts
{
    public interface IAuthService
    {
        Task<CASValidationResponse> ValidateCASTicketAsync(string service, string ticket);
    }
}
