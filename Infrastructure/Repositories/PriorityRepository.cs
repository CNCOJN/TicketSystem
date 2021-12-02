using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Dapper;
using Domain.Models;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class PriorityRepository: IPriorityRepository
    {
        private readonly IDatabaseConnection _conn;

        public PriorityRepository(IDatabaseConnection conn)
        {
            _conn = conn;
        }

        public async Task<IEnumerable<Priority>> GetAllPriorities(CancellationToken cancellationToken = default)
        {
            string sql =
@"
SELECT
	[Id] AS PriorityId,
	[Name] AS PriorityName,
	[CreatedDate],
	[EditedDate]
FROM [Priority]
";
            using SqlConnection db = new SqlConnection(_conn.GetConnectionString());
            return await db.QueryAsync<Priority>(new CommandDefinition(sql, cancellationToken: cancellationToken));
        }
    }
}