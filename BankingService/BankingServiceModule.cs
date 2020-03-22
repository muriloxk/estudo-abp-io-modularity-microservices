using AbpMicroRabbit.Banking.Application;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;
using Microsoft.AspNetCore.Builder;
using Volo.Abp.Autofac;
using AbpMicroRabbit.Banking.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using AbpMicroRabbit.Banking.HttpApi;
using Volo.Abp.AspNetCore.Mvc;
using AbpMicroRabbit.Shared.Domain;
using AbpMicroRabbit.Shared.Infra.Bus;
using MediatR;
using System.Reflection;
using AbpMicroRabbit.Banking.Domain;

namespace BankingService
{
    [DependsOn( typeof(BankingApplicationModule),
                typeof(BankingEntityFrameworkModule),
                typeof(BankingHttpApiModule),
                typeof(AbpAutofacModule),
                typeof(AbpEntityFrameworkCoreMySQLModule),
                typeof(AbpEventBusRabbitMqModule),
                typeof(AbpMicroRabbitSharedInfraBusModule)
               )]
    public class BankingServiceModule : AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //Configurações
            var configuration = context.Services.GetConfiguration();

            ConfigurarControllersGeradosAutomaticamente();
            ConfigurarProviderDoEfCore();
            ConfigurarSwagger(context);
            ConfigurarRabbitMQEventBus(context);
        }

        private void ConfigurarRabbitMQEventBus(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IBus, RabbitMQBus>();
            context.Services.AddMediatR(typeof(BankingDomainModule).GetTypeInfo().Assembly);

            Configure<AbpRabbitMqEventBusOptions>(options =>
            {
                options.ClientName = "TestApp1";
                options.ExchangeName = "TestMessages";
            });
        }

        private void ConfigurarSwagger(ServiceConfigurationContext context)
        {
            context.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Banking Microservice", Version = "v1" });
                c.DocInclusionPredicate((docName, description) => true);
                c.CustomSchemaIds(type => type.FullName);
            });
        }

        private void ConfigurarProviderDoEfCore()
        {
            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL();
            });
        }

        private void ConfigurarControllersGeradosAutomaticamente()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(BankingApplicationModule).Assembly);
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

         
            app.UseSwagger();   

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Baking Service Api");
            });

            app.UseMvcWithDefaultRouteAndArea();
        }
    }
}
