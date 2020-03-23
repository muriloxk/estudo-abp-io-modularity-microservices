using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using AbpMicroRabbit.Transfer.EntityFrameworkCore;

namespace TransferLogService.EntityFramework
{
    public class TransferMigrationsDbContext : AbpDbContext<TransferMigrationsDbContext>
    {
        public TransferMigrationsDbContext(DbContextOptions<TransferMigrationsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfiguracaoTransferDb();
            builder.SeedTransferDb();
        }
    }
}
