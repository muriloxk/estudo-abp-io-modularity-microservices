using System.Collections.Generic;
using AbpMicroRabbit.Banking.Domain.Entities;
using AbpMicroRabbit.Transfer.Domain.Repositories;
using AbpMicroRabbit.Transfer.EntityFrameworkCore.Context;
using Volo.Abp.DependencyInjection;

namespace AbpMicroRabbit.Transfer.EntityFrameworkCore.Repositories
{
    public class TransferRepository : ITransferRepository, ITransientDependency
    {
        private readonly ITransferDbContext _transferDbContext;

        public TransferRepository(ITransferDbContext transferDbContext)
        {
            _transferDbContext = transferDbContext;
        }

        public void Add(TransferLog transferLog)
        {
            _transferDbContext.TransferLogs.Add(transferLog);
        }

        public IEnumerable<TransferLog> GetTransfersLog()
        {
            return _transferDbContext.TransferLogs;
        }
    }
}
