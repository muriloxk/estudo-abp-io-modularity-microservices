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
                b.HasData(new TransferLog(fromAccount: "1231231201-313-131", toAccount: "1231231201-313-132", transferAmount: 100),
                          new TransferLog(fromAccount: "1231231201-313-131", toAccount: "1231231201-313-132", transferAmount: 100));
            });
        }
    }
}
