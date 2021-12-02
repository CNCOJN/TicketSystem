using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces
{
    public interface ISeverityRepository
    {
        Task<IEnumerable<Severity>> GetAllSeverities(CancellationToken cancellationToken = default);
    }
}