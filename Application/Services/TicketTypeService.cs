using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TicketTypeService : ITicketTypeService
    {
        private readonly ITicketTypeRepository _ticketTypeRepository;

        public TicketTypeService(ITicketTypeRepository ticketTypeRepository)
        {
            _ticketTypeRepository = ticketTypeRepository;
        }

        public async Task<IEnumerable<TicketType>> GetAllTicketTypes(CancellationToken cancellationToken = default)
        {
            return await _ticketTypeRepository.GetAllTicketTypes(cancellationToken);
        }
    }
}
