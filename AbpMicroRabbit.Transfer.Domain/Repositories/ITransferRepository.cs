using System;
using System.Collections.Generic;
using AbpMicroRabbit.Banking.Domain.Entities;

namespace AbpMicroRabbit.Transfer.Domain.Repositories
{
    public interface ITransferRepository
    {
        void Add(TransferLog transferLog);
        IEnumerable<TransferLog> GetTransfersLog();
    }
}
