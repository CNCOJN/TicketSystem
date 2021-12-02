using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Dapper;
using Domain.Models;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class TicketTypeRepository: ITicketTypeRepository
    {
        private readonly IDatabaseConnection _conn;

        public TicketTypeRepository(IDatabaseConnection conn)
        {
            _conn = conn;
        }

        public async Task<IEnumerable<TicketType>> GetAllTicketTypes(CancellationToken cancellationToken = default)
        {
            string sql =
@"
SELECT
	[Id] AS TicketTypeId,
	[Name] AS TicketTypeName,
	[CreatedDate],
	[EditedDate]
FROM [TicketType]
";
            using SqlConnection db = new SqlConnection(_conn.GetConnectionString());
            return await db.QueryAsync<TicketType>(new CommandDefinition(sql, cancellationToken: cancellationToken));
        }
    }
}