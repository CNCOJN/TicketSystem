using System;

namespace Domain.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
    }
}