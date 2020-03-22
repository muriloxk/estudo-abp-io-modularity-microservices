using System.Collections.Generic;
using AbpMicroRabbit.Banking.Domain.Entities;
using AbpMicroRabbit.Transfer.Application.Contracts;
using AbpMicroRabbit.Transfer.Domain.Repositories;
using Volo.Abp.Application.Services;

namespace AbpMicroRabbit.Transfer.Application.Services
{
    public class TransferLogApplicationService : ApplicationService, ITransferLogApplicationService
    {
        private readonly ITransferRepository _transferRepository;

        public TransferLogApplicationService(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public IEnumerable<TransferLog> GetTransferLog()
        {
            return _transferRepository.GetTransfersLog();
        }
    }
}
