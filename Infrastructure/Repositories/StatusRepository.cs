using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Dapper;
using Domain.Models;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class StatusRepository: IStatusRepository
    {
        private readonly IDatabaseConnection _conn;

        public StatusRepository(IDatabaseConnection conn)
        {
            _conn = conn;
        }

        public async Task<IEnumerable<Status>> GetAllStatuses(CancellationToken cancellationToken = default)
        {
            string sql =
@"
SELECT
	[Id] AS StatusId,
	[Name] AS StatusName,
	[CreatedDate],
	[EditedDate]
FROM [Status]
";
            using SqlConnection db = new SqlConnection(_conn.GetConnectionString());
            return await db.QueryAsync<Status>(new CommandDefinition(sql, cancellationToken: cancellationToken));
        }
    }
}