using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Dapper;
using Domain.Models;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class AccountRepository: IAccountRepository
    {
        private readonly IDatabaseConnection _conn;

        public AccountRepository(IDatabaseConnection conn)
        {
            _conn = conn;
        }

        public async Task<Account> GetAccountByUsernameAndPassWord(
            Account account, CancellationToken cancellationToken = default)
        {
            string sql =
@"
SELECT
	[Id] AS AccountId,
	[Name],
	[UserName],
	[PassWord],
	[RoleId],
	[CreatedDate],
	[EditedDate]
FROM [Account]
WHERE [UserName] = @UserName
	AND [Password] = @PassWord
";
            using SqlConnection db = new SqlConnection(_conn.GetConnectionString());
            return await db.QueryFirstOrDefaultAsync<Account>(
                new CommandDefinition(
                    sql, 
                    cancellationToken: cancellationToken, 
                    parameters: new { UserName = account.UserName, PassWord = account.PassWord }));
        }

        public async Task<int> PostAccount(Account account, CancellationToken cancellationToken = default)
        {
            string sql =
@"
INSERT INTO [Account] (
	[Name],
	[UserName],
	[Password],
	[RoleId]
)
VALUES (
	@Name,
	@UserName,
	@Password,
	@RoleId
)
";
            using SqlConnection db = new(_conn.GetConnectionString());
            return await db.ExecuteAsync(
                new CommandDefinition(
                    sql, 
                    cancellationToken: cancellationToken, 
                    parameters: account));
        }
    }
}