using AbpMicroRabbit.Banking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AbpMicroRabbit.Transfer.EntityFrameworkCore.Context
{
    public interface ITransferDbContext : IEfCoreDbContext
    {
        DbSet<TransferLog> TransferLogs { get; }
    }
}
