﻿using System;
using Microsoft.VisualBasic;

namespace Domain.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Summary { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
        public int TicketTypeId{ get; set; }
        public string? TicketTypeName { get; set; }
        public int PriorityId { get; set; }
        public string? PriorityName { get; set; }
        public int SeverityId { get; set; }
        public string? SeverityName { get; set; }
        public int StatusId { get; set; }
        public string? StatusName { get; set; }
        public int CreatedBy { get; set; }
        public string? CreaterName { get; set; }
        public string? CreaterRoleName { get; set; }
        public int EditedBy { get; set; }
        public string? EditerName { get; set; }
        public string? EditerRoleName { get; set; }
    }
}