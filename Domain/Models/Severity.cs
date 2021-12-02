using System;

namespace Domain.Models
{
    public class Severity
    {
        public int SeverityId { get; set; }
        public string? SeverityName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}