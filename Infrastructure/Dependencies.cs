using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Dependencies
    {
        public static void AddInfrastructureDependencies (this IServiceCollection services)
        {
            services.AddTransient<IDatabaseConnection, DatabaseConnection>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IPriorityRepository, PriorityRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ISeverityRepository, SeverityRepository>();
            services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }
    }
}