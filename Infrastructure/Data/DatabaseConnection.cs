using Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class DatabaseConnection: IDatabaseConnection
    {
        private readonly IConfiguration _configuration;

        public DatabaseConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("TicketSystemDb");
        }
    }
}