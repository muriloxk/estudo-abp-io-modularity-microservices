using System.Threading.Tasks;
using AbpMicroRabbit.Shared.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.RabbitMQ;

namespace AbpMicroRabbit.Shared.Infra.Bus
{
    public class RabbitMQBusComMediator : RabbitMqDistributedEventBus, IBusMediatR
    {
        private readonly IMediator _mediator;

        public RabbitMQBusComMediator(
                IMediator mediator,
                IOptions<AbpRabbitMqEventBusOptions> options,
                IConnectionPool connectionPool,
                IRabbitMqSerializer serializer,
                IServiceScopeFactory serviceScopeFactory,
                IOptions<AbpDistributedEventBusOptions> distributedEventBusOptions,
                IRabbitMqMessageConsumerFactory messageConsumerFactory)
            :   base(options,
                     connectionPool,
                     serializer,
                     serviceScopeFactory,
                     distributedEventBusOptions,
                     messageConsumerFactory)
        {
            _mediator = mediator;
        }

        public Task<bool> SendCommand<T>(T command) where T : IRequest<bool>
        {
            return _mediator.Send(command);
        }

        //Observações:
        // 1. A classe implementada no RabbitMqDistributedEventBus é fraca e acoplada a um fluxo de exchange,
        // que aparentemente começaram apenas há 2 meses
        // e o correto seria criar um utilizando a biblioteca do proprio framework,  
        // https://github.com/abpframework/abp/tree/dev/framework/src/Volo.Abp.RabbitMQ/Volo/Abp/RabbitMQ
        // ou as vezes até melhorar a classe RabbitMqDistributedEventBus e contribuir.

        // 2. Ainda não há uma implementação com MediaTR o que ajudaria muito nos handlers de comandos
        // chamados por serviços em CQRS. Há uma issue com prioridade alta aberta no github:
        // CQRS infrastructure #57: https://github.com/abpframework/abp/issues/57
    }
}
