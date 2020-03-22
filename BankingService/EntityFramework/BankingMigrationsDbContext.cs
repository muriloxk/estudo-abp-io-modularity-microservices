using AbpMicroRabbit.Banking.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BankingService.EntityFramework
{
    public class BankingMigrationsDbContext : AbpDbContext<BankingMigrationsDbContext>
    {
        public BankingMigrationsDbContext(DbContextOptions<BankingMigrationsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigureBankingDb();
            //builder.SeedBankingDb();
        }
    }
}
