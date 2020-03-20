using System.Threading;
using System.Threading.Tasks;
using AbpMicroRabbit.Banking.Domain.Commands;
using AbpMicroRabbit.Banking.Domain.Events;
using MediatR;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;

namespace AbpMicroRabbit.Banking.Domain.Handlers
{
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
    {
        private readonly IDistributedEventBus _bus;
        private readonly IObjectMapper _objectMapper;

        public TransferCommandHandler(IDistributedEventBus bus, IObjectMapper objectMapper)
        {
            _bus = bus;
            _objectMapper = objectMapper;
        }

        public async Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            await _bus.PublishAsync(_objectMapper.Map<CreateTransferCommand, TransferCreatedEvent>(request));
            return true;
        }
    }
}
