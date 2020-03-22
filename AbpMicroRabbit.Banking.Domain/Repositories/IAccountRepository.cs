using System;
using System.Collections.Generic;
using AbpMicroRabbit.Banking.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace AbpMicroRabbit.Banking.Domain.Repositories
{
    public interface IAccountRepository : IRepository
    {
        IEnumerable<Account> GetAccounts();
    }
}
