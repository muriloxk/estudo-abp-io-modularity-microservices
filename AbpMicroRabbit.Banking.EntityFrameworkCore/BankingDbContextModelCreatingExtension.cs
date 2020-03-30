using System;
using AbpMicroRabbit.Banking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace AbpMicroRabbit.Banking.EntityFrameworkCore
{
    public static class BankingDbContextModelCreatingExtension
    {
        public static void ConfigureBankingDb(this ModelBuilder builder)
        {
            builder.Entity<Account>(b =>
            {
                b.ToTable("Accounts");
                b.ConfigureExtraProperties();
            });
        }

        public static void SeedBankingDb(this ModelBuilder builder)
        {
            builder.Entity<Account>(b =>
            {
                b.HasData(new Account(accountType: "Poupança", accountBalance: 100) { TenantId = Guid.Parse("9fcc4ade-21e5-4686-a1ba-36aff25e5d0f") },
                          new Account(accountType: "Credito", accountBalance: 200) { TenantId = Guid.Parse("9fcc4ade-21e5-4686-a1ba-36aff25e5d0f") },
                          new Account(accountType: "Poupança", accountBalance: 100) { TenantId = Guid.Parse("68d3a738-2918-4b1d-a293-71e9aaff8024") },
                          new Account(accountType: "Credito", accountBalance: 200) { TenantId = Guid.Parse("68d3a738-2918-4b1d-a293-71e9aaff8024") });
            });
        }
    }
}
