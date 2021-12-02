using System;

namespace Domain.Models
{
    public class Status
    {
        public int StatusId { get; set; }
        public string? StatusName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}