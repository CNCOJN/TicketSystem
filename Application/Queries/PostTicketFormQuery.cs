using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class PostTicketFormQuery : IRequest<PostTicketFormInfoDto>
    {
        public class PostTicketFormQueryHander : IRequestHandler<PostTicketFormQuery, PostTicketFormInfoDto>
        {
            private readonly ITicketTypeService _ticketTypeService;
            private readonly IPriorityService _priorityService;
            private readonly ISeverityService _severityService;
            private readonly IStatusService _statusService;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public PostTicketFormQueryHander(
                ITicketTypeService ticketTypeService,
                IPriorityService priorityService,
                ISeverityService severityService,
                IStatusService statusService,
                IMapper mapper, 
                IHttpContextAccessor httpContextAccessor)
            {
                _ticketTypeService = ticketTypeService;
                _priorityService = priorityService;
                _severityService = severityService;
                _statusService = statusService;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<PostTicketFormInfoDto> Handle(PostTicketFormQuery request, CancellationToken cancellationToken)
            {
                string? role = _httpContextAccessor
                    .HttpContext
                    .User
                    .Claims
                    .FirstOrDefault(x => x.Type.ToLowerInvariant() == "role")?
                    .Value
                    .ToLowerInvariant();
                IEnumerable<TicketType>? ticketTypes = await _ticketTypeService.GetAllTicketTypes();
                IEnumerable<Priority>? priorities = await _priorityService.GetAllPriorities();
                IEnumerable<Severity>? severities = await _severityService.GetAllSeverities();
                IEnumerable<Status>? statuses = await _statusService.GetAllStatuses();

                IEnumerable<TicketTypeDto>? ticketTypeDtos = _mapper.Map<IEnumerable<TicketTypeDto>>(ticketTypes);
                IEnumerable<PriorityDto>? priorityDtos = _mapper.Map<IEnumerable<PriorityDto>>(priorities);
                IEnumerable<SeverityDto>? severityDtos = _mapper.Map<IEnumerable<SeverityDto>>(severities);
                IEnumerable<StatusDto>? statusDtos = _mapper.Map<IEnumerable<StatusDto>>(statuses);

                if (role?.ToLowerInvariant() == "qa")
                {
                    ticketTypeDtos = ticketTypeDtos
                        .Where(x => {
                            return x.TicketTypeName?.ToLowerInvariant() == "bug" 
                                   || x.TicketTypeName?.ToLowerInvariant() == "test case";
                        });
                }
                else if (role?.ToLowerInvariant() == "pm")
                {
                    ticketTypeDtos = ticketTypeDtos.Where(x => x.TicketTypeName?.ToLowerInvariant() == "feature request");
                }

                PostTicketFormInfoDto response = new()
                {
                    TicketTypes = ticketTypeDtos,
                    Priorities = priorityDtos,
                    Severities = severityDtos,
                    Statuses = statusDtos
                };
                return response;
            }
        }
    }
}
