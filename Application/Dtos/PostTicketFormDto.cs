using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class PostTicketFormDto
    {
        public int TicketTypeId { get; set; }
        public int PriorityId { get; set; }
        public int SeverityId { get; set; }
        public string? Description { get; set; }
        public string? Summary { get; set; }
        public int StatusId { get; set; }
    }
}
