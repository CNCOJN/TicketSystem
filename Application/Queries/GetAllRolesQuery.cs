using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetAllRolesQuery: IRequest<IEnumerable<RoleDto>>
    {
        public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleDto>>
        {
            private readonly IRoleService _roleService;
            private readonly IMapper _mapper;

            public GetAllRolesQueryHandler(IRoleService roleService, IMapper mapper)
            {
                _roleService = roleService;
                _mapper = mapper;
            }

            public async Task<IEnumerable<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Role>? roles = await _roleService.GetAllRoles(cancellationToken);
                IEnumerable<RoleDto>? roleDtos = _mapper.Map<IEnumerable<RoleDto>>(roles);
                return roleDtos;
            }
        }
    }
}