using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class PostAccountCommand : IRequest<int>
    {
        private readonly RegisterFormDto _registerFormDto;

        public PostAccountCommand(RegisterFormDto registerFormDto)
        {
            _registerFormDto = registerFormDto;
        }

        public class PostAccountCommandHandler : IRequestHandler<PostAccountCommand, int>
        {
            private readonly IMapper _mapper;
            private readonly IAccountService _accountService;
            private readonly IConfiguration _configuration;

            public PostAccountCommandHandler(IMapper mapper, IAccountService accountService, IConfiguration configuration)
            {
                _mapper = mapper;
                _accountService = accountService;
                _configuration = configuration;
            }

            public async Task<int> Handle(PostAccountCommand request, CancellationToken cancellationToken)
            {
                string key = _configuration.GetSection("key").Value;
                Account? account = _mapper.Map<Account>(request._registerFormDto);
                account.EncryptPassword(key);
                int response = await _accountService.PostAccount(account, cancellationToken);
                return response;
            }
        }
    }
}
