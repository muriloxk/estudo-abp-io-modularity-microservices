using AbpMicroRabbit.Banking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AbpMicroRabbit.Banking.EntityFrameworkCore.Context
{
    public class BankingDbContext : AbpDbContext<BankingDbContext>, IBankingContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options) { }

        public virtual DbSet<Account> Accounts { get; set; }
    }
}
