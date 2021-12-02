using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Dapper;
using Domain.Models;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class RoleRepository: IRoleRepository
    {
        private readonly IDatabaseConnection _conn;

        public RoleRepository(IDatabaseConnection conn)
        {
            _conn = conn;
        }

        public async Task<IEnumerable<Role>> GetAllRoles(CancellationToken cancellationToken = default)
        {
            string sql =
@"
SELECT 
	[Id] AS RoleId,
	[Name] AS RoleName,
	[CreatedDate],
	[EditedDate]
FROM [Role]
";
            using SqlConnection db = new(_conn.GetConnectionString());
            return await db.QueryAsync<Role>(new CommandDefinition(sql, cancellationToken: cancellationToken)); 
        }

        public async Task<Role> GetRoleById(int id, CancellationToken cancellationToken = default)
        {
            string sql =
@"
SELECT 
	[Id] AS RoleId,
	[Name] AS RoleName,
	[CreatedDate],
	[EditedDate]
FROM [Role]
WHERE [Id] = @RoleId
";
            using SqlConnection db = new(_conn.GetConnectionString());
            return await db.QueryFirstOrDefaultAsync<Role>(
                new CommandDefinition(
                    sql, 
                    parameters: new { RoleId = id }, 
                    cancellationToken: cancellationToken));
        }
    }
}