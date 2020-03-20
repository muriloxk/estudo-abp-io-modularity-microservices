using AbpMicroRabbit.Banking.Application;
using AbpMicroRabbit.Banking.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;

namespace BankingService
{
    [DependsOn(typeof(AbpAutofacModule),
               typeof(AbpEventBusRabbitMqModule),
               typeof(AbpEntityFrameworkCoreMySQLModule),
               typeof(BankingApplicationModule),
               typeof(BankingEntityFrameworkModule))]
    public class BankingServiceModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //Configurações
            var configuration = context.Services.GetConfiguration();

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL();
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            //??
        }
    }
}
