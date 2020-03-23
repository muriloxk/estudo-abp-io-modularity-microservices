using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbpMicroRabbit.Shared.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AbpMicroRabbit.Shared.Infra.Bus
{
    public class RabbitMQBus : ITransferLogApplicationService
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMQBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory)
        {
            _mediator = mediator;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
            _serviceScopeFactory = serviceScopeFactory;
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
        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);

            if (!_eventTypes.Contains(typeof(T)))
                _eventTypes.Add(typeof(T));

            if (!_handlers.ContainsKey(eventName))
                _handlers.Add(eventName, new List<Type>());

            if(_handlers[eventName].Any(s => s.GetType() == handlerType))
                throw new ArgumentException($"Handler type {handlerType.Name} á está registrado para {eventName}", nameof(handlerType));

            if (_handlers[eventName].Any(s => s.GetType() == handlerType))
                throw new ArgumentException($"Handler type {handlerType.Name} já está registado para {eventName}");

            _handlers[eventName].Add(handlerType);


            StartBasicConsume<T>();
        }

        private void StartBasicConsume<T>() where T : Event
        {
            var factory = new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var eventName = typeof(T).Name;

            channel.QueueDeclare(eventName, false, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;

            channel.BasicConsume(eventName, true, consumer);
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var eventName = @event.RoutingKey;
            var message = Encoding.UTF8.GetString(@event.Body);

            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch(Exception ex)
            {

            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
           if(_handlers.ContainsKey(eventName))
           {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var handlersParaEsseEvento = _handlers[eventName];
                    foreach(var handlerTipo in handlersParaEsseEvento)
                    {
                        var handler = scope.ServiceProvider.GetService(handlerTipo);
                        if (handler == null) continue;
                        var tipoDoEvento = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                        var @event = JsonConvert.DeserializeObject(message, tipoDoEvento);
                        var concreteType = typeof(IEventHandler<>).MakeGenericType(tipoDoEvento);
                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                    }
                }
           }
        }
    }
}
