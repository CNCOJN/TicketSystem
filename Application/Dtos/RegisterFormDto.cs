using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class RegisterFormDto
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? PassWord { get; set; }
        public int RoleId { get; set; }
    }
}
