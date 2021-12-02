using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITicketTypeService
    {
        Task<IEnumerable<TicketType>> GetAllTicketTypes(CancellationToken cancellationToken = default);
    }
}
