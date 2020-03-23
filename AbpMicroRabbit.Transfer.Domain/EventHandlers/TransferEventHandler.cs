using System.Threading.Tasks;
using AbpMicroRabbit.Banking.Domain.Entities;
using AbpMicroRabbit.Shared.Domain;
using AbpMicroRabbit.Transfer.Domain.Events;
using AbpMicroRabbit.Transfer.Domain.Repositories;
using Volo.Abp.DependencyInjection;

namespace AbpMicroRabbit.Transfer.Domain.EventHandlers
{
    public class TransferEventHandler : IEventHandler<TransferCreatedEvent>, ITransientDependency
    {
        private readonly ITransferRepository _transferRepository;

        public TransferEventHandler(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public Task Handle(TransferCreatedEvent @event)
        {
            _transferRepository.Add(new TransferLog(fromAccount: @event.From, toAccount: @event.To, transferAmount: @event.Amount));
            return Task.CompletedTask;
        }
    }
}
