using AbpMicroRabbit.Banking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace AbpMicroRabbit.Banking.EntityFrameworkCore.Context
{
    [ConnectionStringName("BankingDb")]
    public class BankingDbContext : AbpDbContext<BankingDbContext>, IBankingDbContext, ITransientDependency
    {
        public virtual DbSet<Account> Accounts { get; set; }

        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigureBankingDb();
            //builder.SeedBankingDb();
        }
    }
}
