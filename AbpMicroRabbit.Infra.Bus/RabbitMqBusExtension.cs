using System.Threading.Tasks;
using MediatR;
using Volo.Abp.EventBus.Distributed;

namespace AbpMicroRabbit.Shared.Infra.Bus
{
    public static class RabbitMqBusExtension
    {
        public static IMediator _mediator;

        public static Task<bool> SendCommand<T>(this IDistributedEventBus bus, T command) where T : IRequest<bool>
        {
            return _mediator.Send(command);
        }
    }
}
