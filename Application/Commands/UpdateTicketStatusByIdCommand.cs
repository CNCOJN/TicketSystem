using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class UpdateTicketStatusByIdCommand: IRequest<int>
    {
        private readonly int _id;

        public UpdateTicketStatusByIdCommand(int id)
        {
            _id = id;
        }

        public class UpdateTicketStatusByIdCommandHandler : IRequestHandler<UpdateTicketStatusByIdCommand, int>
        {
            private readonly ITicketService _ticketService;

            public UpdateTicketStatusByIdCommandHandler(ITicketService ticketService)
            {
                _ticketService = ticketService;
            }

            public async Task<int> Handle(UpdateTicketStatusByIdCommand request, CancellationToken cancellationToken)
            {
                int response = await _ticketService.UpdateTicketStatusById(request._id, cancellationToken);
                return response;
            }
        }
    }
}
