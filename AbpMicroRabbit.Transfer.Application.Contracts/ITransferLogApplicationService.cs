using System.Collections.Generic;
using AbpMicroRabbit.Banking.Domain.Entities;
using Volo.Abp.Application.Services;

namespace AbpMicroRabbit.Transfer.Application.Contracts
{
    public interface ITransferLogApplicationService : IApplicationService
    {
        IEnumerable<TransferLog> GetTransferLog();
    }
}
