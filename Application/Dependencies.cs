using Application.Interfaces;
using Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class Dependencies
    {
        public static void ApplicationDependencies(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Dependencies).Assembly);
            services.AddAutoMapper(typeof(Dependencies).Assembly);
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISeverityService, SeverityService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<ITicketTypeService, TicketTypeService>();
            services.AddScoped<IPriorityService, PriorityService>();
        }
    }
}