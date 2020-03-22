using System.Collections.Generic;
using AbpMicroRabbit.Banking.Domain.Entities;
using AbpMicroRabbit.Banking.Domain.Repositories;
using AbpMicroRabbit.Banking.EntityFrameworkCore.Context;
using Volo.Abp.DependencyInjection;

namespace AbpMicroRabbit.Banking.EntityFrameworkCore.Repositories
{

    public class AccountRepository : IAccountRepository, ITransientDependency
    {
        private readonly IBankingDbContext _context;

        public AccountRepository(IBankingDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Account> GetAccounts()
        {
            return _context.Accounts;
        }
    }
}
