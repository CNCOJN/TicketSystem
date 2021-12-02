using System.Threading;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByUsernameAndPassWord(Account account, CancellationToken cancellationToken = default);
        Task<int> PostAccount(Account account, CancellationToken cancellationToken = default);
    }
}