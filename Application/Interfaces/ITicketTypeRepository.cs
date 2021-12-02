using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces
{
    public interface ITicketTypeRepository
    {
        Task<IEnumerable<TicketType>> GetAllTicketTypes(CancellationToken cancellationToken = default);
    }
}