using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Dapper;
using Domain.Models;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class TicketRepository: ITicketRepository
    {
        private readonly IDatabaseConnection _conn;

        public TicketRepository(IDatabaseConnection conn)
        {
            _conn = conn;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsByPage(
	        int page, int rowsPerPage, CancellationToken cancellationToken = default)
        {
            string sql = 
@"
SELECT
	t.[Id],
	t.[Description],
	t.[Summary],
	t.[CreatedDate],
	t.[EditedDate],
	t.[TicketTypeId],
	tp.[Name] AS TicketTypeName,
	t.[PriorityId],
	p.[Name] AS PriorityName,
	t.[SeverityId],
	sy.[Name] AS SeverityName,
	t.[StatusId],
	ss.[Name] AS StatusName,
	t.[CreatedBy],
	ca.[Name] AS CreaterName,
	cr.[Name] AS CreaterRoleName,
	t.[EditedBy],
	ea.[Name] AS EditerName,
	er.[Name] AS EditerRoleName
FROM Ticket t
INNER JOIN [TicketType] tp ON t.[TicketTypeId] = tp.Id
INNER JOIN [Priority] p ON t.PriorityId = p.Id
INNER JOIN [Severity] sy ON t.SeverityId = sy.Id
INNER JOIN [Status] ss ON t.StatusId = ss.Id
LEFT JOIN [Account] ca on t.[CreatedBy] = ca.[Id]
LEFT JOIN [Role] cr on ca.[RoleId] = cr.[Id]
LEFT JOIN [Account] ea on t.[EditedBy] = ea.[Id]
LEFT JOIN [Role] er on ea.[RoleId] = er.[Id]
ORDER BY t.Id
OFFSET @skipRows ROWS
FETCH NEXT @getRows ROWS ONLY
";
            using SqlConnection db = new(_conn.GetConnectionString());
            return await db.QueryAsync<Ticket>(
	            new CommandDefinition(
		            sql,
		            cancellationToken: cancellationToken,
		            parameters: new {skipRows = (page - 1) * rowsPerPage, getRows = rowsPerPage}));
        }

        public async Task<int> GetTotalTicketsCount(CancellationToken cancellationToken)
        {
	        string sql =
		        @"
SELECT COUNT(*)
FROM Ticket t
INNER JOIN [TicketType] tp ON t.[TicketTypeId] = tp.Id
INNER JOIN [Priority] p ON t.PriorityId = p.Id
INNER JOIN [Severity] sy ON t.SeverityId = sy.Id
INNER JOIN [Status] ss ON t.StatusId = ss.Id
LEFT JOIN [Account] ca on t.[CreatedBy] = ca.[Id]
LEFT JOIN [Role] cr on ca.[RoleId] = cr.[Id]
LEFT JOIN [Account] ea on t.[EditedBy] = ea.[Id]
LEFT JOIN [Role] er on ea.[RoleId] = er.[Id]
";
	        using SqlConnection db = new SqlConnection(_conn.GetConnectionString());
	        return await db.QueryFirstOrDefaultAsync<int>(new CommandDefinition(sql, cancellationToken: cancellationToken));
        }

        public async Task<Ticket> GetTicketById(int id, CancellationToken cancellationToken = default)
        {
	        string sql = 
		        @"
SELECT
	t.[Id],
	t.[Description],
	t.[Summary],
	t.[CreatedDate],
	t.[EditedDate],
	t.[TicketTypeId],
	tp.[Name] AS TicketTypeName,
	t.[PriorityId],
	p.[Name] AS PriorityName,
	t.[SeverityId],
	sy.[Name] AS SeverityName,
	t.[StatusId],
	ss.[Name] AS StatusName,
	t.[CreatedBy],
	ca.[Name] AS CreaterName,
	cr.[Name] AS CreaterRoleName,
	t.[EditedBy],
	ea.[Name] AS EditerName,
	er.[Name] AS EditerRoleName
FROM Ticket t
INNER JOIN [TicketType] tp ON t.[TicketTypeId] = tp.Id
INNER JOIN [Priority] p ON t.PriorityId = p.Id
INNER JOIN [Severity] sy ON t.SeverityId = sy.Id
INNER JOIN [Status] ss ON t.StatusId = ss.Id
LEFT JOIN [Account] ca on t.[CreatedBy] = ca.[Id]
LEFT JOIN [Role] cr on ca.[RoleId] = cr.[Id]
LEFT JOIN [Account] ea on t.[EditedBy] = ea.[Id]
LEFT JOIN [Role] er on ea.[RoleId] = er.[Id]
WHERE t.[Id] = @Id
";
	        using SqlConnection db = new SqlConnection(_conn.GetConnectionString());
	        IEnumerable<Ticket>? response =
		        await db.QueryAsync<Ticket>(
			        new CommandDefinition(sql, cancellationToken: default,
				        parameters: new {Id = id}));
	        return response.FirstOrDefault();
        }

        public async Task<int> PostTicket(Ticket ticket, CancellationToken cancellationToken = default)
        {
	        string sql =
@"
INSERT INTO [Ticket] (
	[TicketTypeId],
	[PriorityId],
	[SeverityId],
	[Description],
	[Summary],
	[StatusId],
	[CreatedBy]
)
VALUES (
	@TicketTypeId,
	@PriorityId,
	@SeverityId,
	@Description,
	@Summary,
	@StatusId,
	@CreatedBy
)
";
	        using SqlConnection db = new SqlConnection(_conn.GetConnectionString());
	        return await db.ExecuteAsync(
		        new CommandDefinition(
			        sql, 
			        cancellationToken: cancellationToken,
			        parameters: ticket));
        }

        public async Task<int> UpdateTicketById(Ticket ticket, CancellationToken cancellationToken = default)
        {
	        string sql =
@"
UPDATE [Ticket]
SET
	[RoleId] = @RoleId,
	[TicketTypeId] = @TicketTypeId,
	[PriorityId] = @PriorityId,
	[SeverityId] = @SeverityId,
	[Description] = @Description,
	[Summary] = @Summary,
	[StatusId] = @StatusId,
	[EditedDate] = GETDATE()
WHERE [Id] = @Id
";
	        using SqlConnection db = new SqlConnection(_conn.GetConnectionString());
	        return await db.ExecuteAsync(
		        new CommandDefinition(
			        sql,
			        cancellationToken: cancellationToken,
			        parameters: ticket));
        }

        public async Task<int> DeleteTicketById(int id, CancellationToken cancellationToken = default)
        {
	        string sql = 
@"
DELETE [Ticket]
WHERE [Id] = @Id
";
	        using SqlConnection db = new SqlConnection(_conn.GetConnectionString());
	        return await db.ExecuteAsync(
		        new CommandDefinition(
			        sql, 
			        cancellationToken: cancellationToken,
			        parameters: new {Id = id}));
        }

        public async Task<int> UpdateTicketStatusById(int id, CancellationToken cancellationToken = default)
        {
			string sql =
@"
UPDATE [Ticket]
SET [StatusId] = 2
WHERE [Id] = @Id
";
			using SqlConnection db = new(_conn.GetConnectionString());
			return await db.ExecuteAsync(new CommandDefinition(sql, cancellationToken: cancellationToken, parameters: new { @Id = id }));
        }
    }
}