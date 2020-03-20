using MediatR;

namespace AbpMicroRabbit.Shared.Domain
{
    public abstract class Message : IRequest<bool>
    {
        public Message()
        {
        }
    }
}
