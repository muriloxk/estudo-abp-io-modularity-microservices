using System;
using AbpMicroRabbit.Banking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace AbpMicroRabbit.Transfer.EntityFrameworkCore
{
    public static class TransferDbContextModelCreatingExtension
    {
        public static void ConfiguracaoTransferDb(this ModelBuilder builder)
        {
            builder.Entity<TransferLog>(b =>
            {
                b.ToTable("TransferLogs");
                b.ConfigureExtraProperties();
            });
        }

        public static void SeedTransferDb(this ModelBuilder builder)
        {
            builder.Entity<TransferLog>(b =>
            {
                b.HasData(new TransferLog(fromAccount: "1231231201-313-131", toAccount: "1231231201-313-132", transferAmount: 100) { TenantId = Guid.Parse("9fcc4ade-21e5-4686-a1ba-36aff25e5d0f") },
                          new TransferLog(fromAccount: "1231231201-313-131", toAccount: "1231231201-313-132", transferAmount: 100) { TenantId = Guid.Parse("9fcc4ade-21e5-4686-a1ba-36aff25e5d0f") },
                          new TransferLog(fromAccount: "1231231201-313-131", toAccount: "1231231201-313-132", transferAmount: 100) { TenantId = Guid.Parse("68d3a738-2918-4b1d-a293-71e9aaff8024") },
                          new TransferLog(fromAccount: "1231231201-313-131", toAccount: "1231231201-313-132", transferAmount: 100) { TenantId = Guid.Parse("68d3a738-2918-4b1d-a293-71e9aaff8024") });
            });
        }
    }
}
