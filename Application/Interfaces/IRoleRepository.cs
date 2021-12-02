using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRoles(CancellationToken cancellationToken = default);
        Task<Role> GetRoleById(int id, CancellationToken cancellationToken = default);
    }
}