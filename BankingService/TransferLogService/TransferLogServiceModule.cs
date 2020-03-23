using System.Reflection;
using AbpMicroRabbit.Shared.Domain;
using AbpMicroRabbit.Shared.Infra.Bus;
using AbpMicroRabbit.Transfer.Application;
using AbpMicroRabbit.Transfer.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace TransferLogService
{
    //[DependsOn(typeof())]
    public class TransferLogServiceModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //base.ConfigureServices(context);

            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(TransferApplicationModule).Assembly);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL();
            });


            context.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TransferLog Microservice", Version = "v1" });
                c.DocInclusionPredicate((docName, description) => true);
                c.CustomSchemaIds(type => type.FullName);
            });

            context.Services.AddSingleton<IBus, RabbitMQBus>();
            context.Services.AddMediatR(typeof(TransferDomainModule).GetTypeInfo().Assembly);
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
        }
    }
}
