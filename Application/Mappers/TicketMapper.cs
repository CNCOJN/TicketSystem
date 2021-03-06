using Application.Dtos;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class TicketMapper: Profile
    {
        public TicketMapper()
        {
            CreateMap<Ticket, TicketDto>();
            CreateMap<PostTicketFormDto, Ticket>();
        }
    }
}
