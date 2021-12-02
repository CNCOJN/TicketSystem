using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Dapper;
using Domain.Models;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class SeverityRepository: ISeverityRepository
    {
        private readonly IDatabaseConnection _conn;

        public SeverityRepository(IDatabaseConnection conn)
        {
            _conn = conn;
        }

        public async Task<IEnumerable<Severity>> GetAllSeverities(CancellationToken cancellationToken = default)
        {
            string sql =
@"
SELECT
	[Id] AS SeverityId,
	[Name] AS Severityname,
	[CreatedDate],
	[EditedDate]
FROM [Severity]
";
            using SqlConnection db = new SqlConnection(_conn.GetConnectionString());
            return await db.QueryAsync<Severity>(new CommandDefinition(sql,
                cancellationToken: cancellationToken));
        }
    }
}