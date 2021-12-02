using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IPriorityRepository
    {
        Task<IEnumerable<Priority>> GetAllPriorities(CancellationToken cancellationToken = default);
    }
}