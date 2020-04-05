using System.Reflection;
using AbpMicroRabbit.Shared.Domain;
using AbpMicroRabbit.Shared.Infra.Bus;
using AbpMicroRabbit.Transfer.Application;
using AbpMicroRabbit.Transfer.Domain;
using AbpMicroRabbit.Transfer.Domain.EventHandlers;
using AbpMicroRabbit.Transfer.Domain.Events;
using AbpMicroRabbit.Transfer.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace TransferLogService
{
    [DependsOn(typeof(TransferApplicationModule),
               typeof(AbpAspNetCoreMvcModule),
               typeof(TransferApplicationModule),
               typeof(TransferEntityFrameworkModule),
               typeof(AbpAutofacModule),
               typeof(AbpEntityFrameworkCoreMySQLModule),
               typeof(AbpMicroRabbitSharedInfraBusModule),
               typeof(AbpAspNetCoreMultiTenancyModule))]
    public class TransferLogServiceModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //base.ConfigureServices(context);

            ConfigurarMultiTenancy();
            ConfigurarControllersGeradosAutomaticamente();
            ConfigurarProviderDoEfCore();
            ConfigurarSwagger(context);
            ConfigurarRabbitMQEventBus(context);
        }

        private void ConfigurarMultiTenancy()
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = true;
            });
        }

        private static void ConfigurarRabbitMQEventBus(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<ITransferLogApplicationService, RabbitMQBus>();
            context.Services.AddMediatR(typeof(TransferDomainModule).GetTypeInfo().Assembly);
        }

        private static void ConfigurarSwagger(ServiceConfigurationContext context)
        {
            context.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TransferLog Microservice", Version = "v1" });
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
                options.ConventionalControllers.Create(typeof(TransferApplicationModule).Assembly, opts =>
                {
                    opts.RootPath = "transferlogservice";
                });
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "TransferLogService Api");
            });

            app.UseMvcWithDefaultRouteAndArea();

            ConfigureEventBus(app);
        }

        private static void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<ITransferLogApplicationService>();
            eventBus.Subscribe<TransferCreatedEvent, TransferEventHandler>();
        }
    }
}
