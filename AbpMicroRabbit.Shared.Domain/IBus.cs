using System.Threading.Tasks;
using MediatR;
using Volo.Abp.EventBus.Distributed;

namespace AbpMicroRabbit.Shared.Domain
{
    public interface IBus 
    {
        Task<bool> SendCommand<T>(T command) where T : IRequest<bool>;
        void Publish<T>(T @event) where T : Event;
    }

    public interface IBusMediatR : IDistributedEventBus
    {
        Task<bool> SendCommand<T>(T command) where T : IRequest<bool>;
    }
}
