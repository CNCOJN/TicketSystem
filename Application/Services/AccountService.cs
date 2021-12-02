using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<int> PostAccount(Account account, CancellationToken cancellationToken = default)
        {
            return await _accountRepository.PostAccount(account, cancellationToken);
        }

        public async Task<Account> GetAccountByUsernameAndPassWord(Account account, CancellationToken cancellationToken = default)
        {
            return await _accountRepository.GetAccountByUsernameAndPassWord(account, cancellationToken);
        }
    }
}
