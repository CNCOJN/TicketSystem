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
    public class AccountMapper: Profile
    {
        public AccountMapper()
        {
            CreateMap<RegisterFormDto, Account>();
            CreateMap<VerifyDto, Account>();
        }
    }
}
