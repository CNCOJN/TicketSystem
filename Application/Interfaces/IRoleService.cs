using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRoles(CancellationToken cancellationToken = default);
        Task<Role> GetRoleById(int id, CancellationToken cancellationToken = default);
    }
}
