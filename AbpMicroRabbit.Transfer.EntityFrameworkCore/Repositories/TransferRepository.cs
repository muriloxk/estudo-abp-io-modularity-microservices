using System.Collections.Generic;
using System.Linq;
using AbpMicroRabbit.Banking.Domain.Entities;
using AbpMicroRabbit.Transfer.Domain.Repositories;
using AbpMicroRabbit.Transfer.EntityFrameworkCore.Context;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace AbpMicroRabbit.Transfer.EntityFrameworkCore.Repositories
{
    public class TransferRepository : ITransferRepository,
                                      ITransientDependency
    {
        private readonly ITransferDbContext _transferDbContext;
        private readonly ICurrentTenant _currentTenant; 

        public TransferRepository(ITransferDbContext transferDbContext,
                                  ICurrentTenant currentTenant)
        {
            _transferDbContext = transferDbContext;
            _currentTenant = currentTenant;
        }

        public void Add(TransferLog transferLog)
        {
            _transferDbContext.TransferLogs.Add(transferLog);
            _transferDbContext.SaveChanges();
        }

        public IEnumerable<TransferLog> GetTransfersLog()
        {
            return _transferDbContext.TransferLogs
                                     .Where(transfer => transfer.TenantId == _currentTenant.Id)
                                     .ToList();
        }
    }
}
