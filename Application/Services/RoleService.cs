using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<Role>> GetAllRoles(CancellationToken cancellationToken = default)
        {
            return await _roleRepository.GetAllRoles(cancellationToken);
        }

        public async Task<Role> GetRoleById(int id, CancellationToken cancellationToken = default)
        {
            return await _roleRepository.GetRoleById(id, cancellationToken);
        }
    }
}
