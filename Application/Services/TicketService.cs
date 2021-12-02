using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class TicketService: ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsByPage(int page, int rowsPerPage, CancellationToken cancellationToken = default)
        {
            return await _ticketRepository.GetAllTicketsByPage(page, rowsPerPage, cancellationToken);
        }

        public async Task<int> GetTotalTicketsCount(CancellationToken cancellationToken = default)
        {
            return await _ticketRepository.GetTotalTicketsCount(cancellationToken);
        }

        public async Task<Ticket> GetTicketById(int id, CancellationToken cancellationToken = default)
        {
            return await _ticketRepository.GetTicketById(id, cancellationToken);
        }

        public async Task<int> PostTicket(Ticket ticket, CancellationToken cancellationToken = default)
        {
            return await _ticketRepository.PostTicket(ticket, cancellationToken);
        }

        public async Task<int> UpdateTicketById(Ticket ticket, CancellationToken cancellationToken = default)
        {
            return await _ticketRepository.UpdateTicketById(ticket, cancellationToken);
        }

        public async Task<int> DeleteTicketById(int id, CancellationToken cancellationToken = default)
        {
            return await _ticketRepository.DeleteTicketById(id, cancellationToken);
        }

        public async Task<int> UpdateTicketStatusById(int id, CancellationToken cancellationToken = default)
        {
            return await _ticketRepository.UpdateTicketStatusById(id, cancellationToken);
        }
    }
}