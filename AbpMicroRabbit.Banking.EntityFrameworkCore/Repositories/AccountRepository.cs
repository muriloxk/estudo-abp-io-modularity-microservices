using System.Collections.Generic;
using AbpMicroRabbit.Banking.Domain.Entities;
using AbpMicroRabbit.Banking.Domain.Repositories;
using Volo.Abp.DependencyInjection;

namespace AbpMicroRabbit.Banking.EntityFrameworkCore.Repositories
{

    public class AccountRepository : IAccountRepository, ITransientDependency
    {
        //private readonly IBankingContext _context;

        //public AccountRepository(IBankingContext context)
        //{
        //    _context = context;
        //}
        

        public IEnumerable<Account> GetAccounts()
        {
            return new List<Account>();
        }
    }
}
