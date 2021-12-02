using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class LogoutCommand: IRequest<bool>
    {
        public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly ILogger<LogoutCommandHandler> _logger;

            public LogoutCommandHandler(IHttpContextAccessor httpContextAccessor, ILogger<LogoutCommandHandler> logger)
            {
                _httpContextAccessor = httpContextAccessor;
                _logger = logger;
            }

            public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await _httpContextAccessor.HttpContext.SignOutAsync("LoginCookie");
                    return true;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "LogoutCommandHandler --ERROR--");
                    return false;
                }
            }
        }
    }
}
