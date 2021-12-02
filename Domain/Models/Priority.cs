using System;

namespace Domain.Models
{
    public class Priority
    {
        public int PriorityId { get; set; }
        public string? PriorityName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}