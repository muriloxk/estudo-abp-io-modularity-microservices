using System.Collections.Generic;
using System.Linq;
using AbpMicroRabbit.Banking.Domain.Entities;
using AbpMicroRabbit.Banking.Domain.Repositories;
using AbpMicroRabbit.Banking.EntityFrameworkCore.Context;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace AbpMicroRabbit.Banking.EntityFrameworkCore.Repositories
{

    public class AccountRepository : IAccountRepository, ITransientDependency
    {
        private readonly IBankingDbContext _context;
        private readonly ICurrentTenant _currentTenant;
        private readonly ILogger<Account> _logger;

        public AccountRepository(IBankingDbContext context, ICurrentTenant currentTenant, ILogger<Account> logger) 
        {
            _context = context;
            _currentTenant = currentTenant;
            _logger = logger;
        }

        public IEnumerable<Account> GetAccounts()
        {
            _logger.LogWarning($"* CURRENT TENANT: * {_currentTenant.Id}");

            return _context.Accounts
                           .Where(account => account.TenantId == _currentTenant.Id)
                           .ToList();
        }
    }
}
