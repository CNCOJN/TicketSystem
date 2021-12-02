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
    public class GetAllTicketsByPageQuery: IRequest<IEnumerable<TicketDto>>
    {
        private readonly int _page;
        private readonly int _totalPages;

        public GetAllTicketsByPageQuery(int page)
        {
            _page = page;
        }

        public class GetAllTicketsByPageQueryHandler : IRequestHandler<GetAllTicketsByPageQuery, IEnumerable<TicketDto>>
        {
            private readonly ITicketService _ticketService;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetAllTicketsByPageQueryHandler(ITicketService ticketService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            {
                _ticketService = ticketService;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<IEnumerable<TicketDto>> Handle(GetAllTicketsByPageQuery request, CancellationToken cancellationToken)
            {
                int page = request._page <= 1 ? 1 : request._page;
                IEnumerable<Ticket>? tickets = await _ticketService.GetAllTicketsByPage(page, 5, cancellationToken);
                IEnumerable<TicketDto>? ticketDtos = _mapper.Map<IEnumerable<TicketDto>>(tickets);
                int totalCount = await _ticketService.GetTotalTicketsCount(cancellationToken);
                int totalPages = (int)Math.Ceiling(totalCount / 5.0);
                _httpContextAccessor.HttpContext.Session.SetInt32("page", page);
                _httpContextAccessor.HttpContext.Session.SetInt32("totalPages", totalPages);
                return ticketDtos;
            }
        }
    }
}
