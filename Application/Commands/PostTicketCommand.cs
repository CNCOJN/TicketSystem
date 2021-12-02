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

namespace Application.Commands
{
    public class PostTicketCommand : IRequest<int>
    {
        private readonly PostTicketFormDto _postTicketFormDto;

        public PostTicketCommand(PostTicketFormDto postTicketFormDto)
        {
            _postTicketFormDto = postTicketFormDto;
        }

        public class PostTicketCommandHandler : IRequestHandler<PostTicketCommand, int>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IMapper _mapper;
            private readonly ITicketService _ticketService;

            public PostTicketCommandHandler(
                IHttpContextAccessor httpContextAccessor, 
                IMapper mapper, 
                ITicketService ticketService)
            {
                _httpContextAccessor = httpContextAccessor;
                _mapper = mapper;
                _ticketService = ticketService;
            }

            public async Task<int> Handle(PostTicketCommand request, CancellationToken cancellationToken)
            {
                string? id = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.ToLowerInvariant() == "id")?.Value;
                if(id is null)
                {
                    return await Task.FromResult(0);
                }
                int accountId = Convert.ToInt32(id);
                PostTicketFormDto? postTicketFormDto = request._postTicketFormDto;
                Ticket ticket = _mapper.Map<Ticket>(postTicketFormDto);
                ticket.CreatedBy = accountId;
                int response = await _ticketService.PostTicket(ticket);
                return response;
            }
        }
    }
}
