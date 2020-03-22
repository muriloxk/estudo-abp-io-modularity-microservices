using System;
using AbpMicroRabbit.Banking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace AbpMicroRabbit.Transfer.EntityFrameworkCore.Context
{
    [ConnectionStringName("BankingDb")]
    public class TransferDbContext : AbpDbContext<TransferDbContext>, ITransferDbContext, ITransientDependency
    {
        public virtual DbSet<TransferLog> TransferLogs { get; set; }

        public TransferDbContext(DbContextOptions<TransferDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfiguracaoTransferDb();
            builder.SeedTransferDb();
        }
    }
}
