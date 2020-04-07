using System.Collections.Generic;
using System.Linq;
using AbpMicroRabbit.Banking.Domain.Entities;
using AbpMicroRabbit.Banking.Domain.Repositories;
using AbpMicroRabbit.Banking.EntityFrameworkCore.Context;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace AbpMicroRabbit.Banking.EntityFrameworkCore.Repositories
{

    public class AccountRepository : IAccountRepository, ITransientDependency
    {
        private readonly IBankingDbContext _context;
        private readonly ICurrentTenant _currentTenant;

        public AccountRepository(IBankingDbContext context, ICurrentTenant currentTenant) 
        {
            _context = context;
            _currentTenant = currentTenant;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _context.Accounts
                           .Where(account => account.TenantId == _currentTenant.Id)
                           .ToList();
        }
    }
}
