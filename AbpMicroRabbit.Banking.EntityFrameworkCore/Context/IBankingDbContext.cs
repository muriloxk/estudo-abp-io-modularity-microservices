using System;
using AbpMicroRabbit.Banking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace AbpMicroRabbit.Banking.EntityFrameworkCore.Context
{
    public interface IBankingDbContext : IEfCoreDbContext
    {
        DbSet<Account> Accounts { get; }
    }
}
