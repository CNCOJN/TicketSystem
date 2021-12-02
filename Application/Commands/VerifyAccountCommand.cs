using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Commands
{
    public class VerifyAccountCommand : IRequest<bool>
    {
        private readonly VerifyDto _verifyDto;

        public VerifyAccountCommand(VerifyDto verifyDto)
        {
            _verifyDto = verifyDto;
        }

        public class VerifyAccountCommandHandler : IRequestHandler<VerifyAccountCommand, bool>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IAccountService _accountService;
            private readonly IMapper _mapper;
            private readonly IConfiguration _configuration;
            private readonly IRoleService _roleService;
            private readonly ILogger<VerifyAccountCommandHandler> _logger;

            public VerifyAccountCommandHandler(
                IHttpContextAccessor httpContextAccessor,
                IAccountService accountService,
                IMapper mapper,
                IRoleService roleService,
                IConfiguration configuration, ILogger<VerifyAccountCommandHandler> logger)
            {
                _httpContextAccessor = httpContextAccessor;
                _accountService = accountService;
                _mapper = mapper;
                _roleService = roleService;
                _configuration = configuration;
                _logger = logger;
            }

            public async Task<bool> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    string key = _configuration.GetSection("key").Value;
                    VerifyDto? verifyDto = request._verifyDto;
                    Account? account = _mapper.Map<Account>(verifyDto);
                    account.EncryptPassword(key);
                    Account? response = await _accountService.GetAccountByUsernameAndPassWord(account, cancellationToken);
                    if (response is null)
                    {
                        return false;
                    }
                    Role? role = await _roleService.GetRoleById(response.RoleId);
                    List<Claim> claims = new()
                    {
                        new Claim("Name", response.Name),
                        new Claim("Role", role.RoleName),
                        new Claim("Id", response.AccountId.ToString()),
                        new Claim(ClaimTypes.Role, role.RoleName)
                    };
                    ClaimsIdentity identity = new(claims, "LoginCookie");
                    ClaimsPrincipal principal = new(identity);
                    await _httpContextAccessor.HttpContext.SignInAsync("LoginCookie", principal);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "VerifyAccountCommandHandler --ERROR--");
                    return false;
                }

                return true;
            }
        }
    }
}