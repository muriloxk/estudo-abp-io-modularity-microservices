using System;
using AbpMicroRabbit.Banking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AbpMicroRabbit.Banking.EntityFrameworkCore.Context
{
    public interface IBankingContext : IEfCoreDbContext
    {
        DbSet<Account> Accounts { get; }
    }
}
