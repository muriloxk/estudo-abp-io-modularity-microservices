using System.Reflection;
using MediatR;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;

namespace AbpMicroRabbit.Shared.Infra.Bus
{
    [DependsOn(typeof(AbpEventBusRabbitMqModule))]
    public class AbpMicroRabbitSharedInfraBusModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
    
        }
    }
}
