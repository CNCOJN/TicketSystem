using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class PostTicketFormInfoDto
    {
        public IEnumerable<TicketTypeDto>? TicketTypes { get; set; }
        public IEnumerable<PriorityDto>? Priorities { get; set; }
        public IEnumerable<SeverityDto>? Severities { get; set; }
        public IEnumerable<StatusDto>? Statuses { get; set; }
    }
}
