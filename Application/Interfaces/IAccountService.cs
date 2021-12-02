using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<int> PostAccount(Account account, CancellationToken cancellationToken = default);
        Task<Account> GetAccountByUsernameAndPassWord(Account account, CancellationToken cancellationToken = default);
    }
}
