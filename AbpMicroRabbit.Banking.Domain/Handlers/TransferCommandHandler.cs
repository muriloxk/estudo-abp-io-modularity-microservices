using System.Threading;
using System.Threading.Tasks;
using AbpMicroRabbit.Banking.Domain.Commands;
using AbpMicroRabbit.Banking.Domain.Events;
using AbpMicroRabbit.Shared.Domain;
using MediatR;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;

namespace AbpMicroRabbit.Banking.Domain.Handlers
{
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
    {
        private readonly IBus _bus;
        private readonly IObjectMapper _objectMapper;

        public TransferCommandHandler(IBus bus, IObjectMapper objectMapper)
        {
            _bus = bus;
            _objectMapper = objectMapper;
        }

        public  Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
             _bus.Publish<TransferCreatedEvent>( _objectMapper.Map<CreateTransferCommand, TransferCreatedEvent>(request));
            return Task.FromResult(true);
        }
    }
}
