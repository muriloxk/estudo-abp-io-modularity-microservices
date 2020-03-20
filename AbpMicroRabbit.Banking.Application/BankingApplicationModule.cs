using AbpMicroRabbit.Banking.Application.Contracts;
using AbpMicroRabbit.Banking.Domain;
using AbpMicroRabbit.Shared.Infra.Bus;
using Volo.Abp.Application;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;

namespace AbpMicroRabbit.Banking.Application
{
    [DependsOn(typeof(BankingDomainModule),
               typeof(AbpDddApplicationModule),
               typeof(BankingApplicationContractsModule),
               typeof(AbpAutoMapperModule),
               typeof(AbpEventBusRabbitMqModule),
               typeof(AbpAutofacModule),
               typeof(AbpMicroRabbitSharedInfraBusModule))]
    public class BankingApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<BookStoreApplicationAutoMapperProfile>(true);
            });

            Configure<AbpRabbitMqEventBusOptions>(options =>
            {
                options.ClientName = "TestApp1";
                options.ExchangeName = "TestMessages";
            });
        }
    }
}
