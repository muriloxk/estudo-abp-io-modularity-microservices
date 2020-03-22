using System.Text;
using System.Threading.Tasks;
using AbpMicroRabbit.Shared.Domain;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace AbpMicroRabbit.Shared.Infra.Bus
{
    public class RabbitMQBus : IBus
    {
        private readonly IMediator _mediator;

        public RabbitMQBus(IMediator mediator) : base()
        {
            _mediator = mediator;
        }

        public Task<bool> SendCommand<T>(T command) where T : IRequest<bool>
        {
            return _mediator.Send(command);
        }

        public void Publish<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var eventName = @event.GetType().Name;

                channel.QueueDeclare(eventName, false, false, false, null);

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("", eventName, null, body);
            }
        }

        // TODO: Subscribe entre outras implementações de registo de eventos
        // por injeção de dependencia.
    }
}
