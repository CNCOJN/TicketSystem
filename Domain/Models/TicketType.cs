using System;

namespace Domain.Models
{
    public class TicketType
    {
        public int TicketTypeId { get; set; }
        public string? TicketTypeName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}