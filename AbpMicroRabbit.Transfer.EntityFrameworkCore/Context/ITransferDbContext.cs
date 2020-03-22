using AbpMicroRabbit.Banking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AbpMicroRabbit.Transfer.EntityFrameworkCore.Context
{
    public interface ITransferDbContext
    {
        DbSet<TransferLog> TransferLogs { get; }
    }
}
