using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAllTicketsByPage(int page, int rowsPerPage, CancellationToken cancellationToken = default);
        Task<int> GetTotalTicketsCount(CancellationToken cancellationToken = default);
        Task<Ticket> GetTicketById(int id, CancellationToken cancellationToken = default);
        Task<int> PostTicket(Ticket ticket, CancellationToken cancellationToken = default);
        Task<int> UpdateTicketById(Ticket ticket, CancellationToken cancellationToken = default);
        Task<int> DeleteTicketById(int id, CancellationToken cancellationToken = default);
        Task<int> UpdateTicketStatusById(int id, CancellationToken cancellationToken = default);
    }
}