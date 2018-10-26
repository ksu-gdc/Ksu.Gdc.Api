using System.Collections.Generic;
using System.Threading.Tasks;

using Ksu.Gdc.Api.Core.Models;

namespace Ksu.Gdc.Api.Core.Contracts
{
    public interface IUtilityService
    {
        bool IsPaginationRequest(int pageNumber, int pageSize);

        List<T> Paginate<T>(List<T> collection, int pageNumber, int pageSize);
    }
}
