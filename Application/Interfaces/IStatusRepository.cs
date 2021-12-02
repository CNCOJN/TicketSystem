using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IStatusRepository
    {
        Task<IEnumerable<Status>> GetAllStatuses(CancellationToken cancellationToken = default);
    }
}