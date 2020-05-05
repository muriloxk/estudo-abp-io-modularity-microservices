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
using Volo.Abp.AspNetCore.Mvc;
using AbpMicroRabbit.Shared.Domain;
using AbpMicroRabbit.Shared.Infra.Bus;
using MediatR;
using System.Reflection;
using AbpMicroRabbit.Banking.Domain;
using AbpMicroRabbit.Banking.Application.Contracts;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.Threading;
using Volo.Abp.Data;
using Volo.Abp.PermissionManagement.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Volo.Abp.Security.Claims;
using System.Collections.Generic;
using System.Linq;

namespace BankingService
{
    [DependsOn(typeof(AbpAutofacModule),
               typeof(AbpAspNetCoreMvcModule),
               typeof(BankingApplicationContractsModule),
               typeof(BankingApplicationModule),
               typeof(BankingEntityFrameworkModule),
               typeof(AbpEntityFrameworkCoreMySQLModule),
               typeof(AbpPermissionManagementDomainIdentityModule),
               typeof(AbpPermissionManagementEntityFrameworkCoreModule),
               typeof(AbpEventBusRabbitMqModule),
               typeof(AbpMicroRabbitSharedInfraBusModule),
               typeof(AbpTenantManagementEntityFrameworkCoreModule),
               typeof(AbpAspNetCoreMultiTenancyModule))]
    public class BankingServiceModule : AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddAuthentication("Bearer")
                            .AddIdentityServerAuthentication(options =>
                            {
                                options.Authority = configuration["AuthServer:Authority"];
                                options.ApiName = configuration["AuthServer:ApiName"];
                                options.RequireHttpsMetadata = false;
                                //options.LegacyAudienceValidation = true;
                            });

            ConfigurarMultiTenancy();
            ConfigurarControllersGeradosAutomaticamente();
            ConfigurarProviderDoEfCore();
            ConfigurarSwagger(context);
            ConfigurarRabbitMQEventBus(context);
            ConfigurarCacheNoRedis(context, configuration);
        }

        private static void ConfigurarCacheNoRedis(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis:Configuration"];
            });
        }

        private void ConfigurarMultiTenancy()
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = true;
            });


            Configure<AbpTenantResolveOptions>(options =>
            {
                options.TenantResolvers.Insert(1, new HeaderTenantResolveContributor());
            });
        }

        private void ConfigurarRabbitMQEventBus(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<ITransferLogApplicationService, RabbitMQBus>();
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
                options.ConventionalControllers.Create(typeof(BankingApplicationModule).Assembly, opts =>
                {
                    opts.RootPath = "bankingservice";
                });
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();


            //TODO: https://github.com/abpframework/abp/issues/2001
            app.Use(async (ctx, next) =>
            {
                var user = ctx.User;
                var currentPrincipalAccessor = ctx.RequestServices.GetRequiredService<ICurrentPrincipalAccessor>();
                var map = new Dictionary<string, string>()
                {
                    { "sub", AbpClaimTypes.UserId },
                    { "role", AbpClaimTypes.Role },
                    { "email", AbpClaimTypes.Email },
                };
                var mapClaims = currentPrincipalAccessor.Principal.Claims.Where(p => map.Keys.Contains(p.Type)).ToList();
                //mapClaims.Add(new Claim("role", "admin", null, mapClaims[0].Issuer)); //TODO: REMOVER
                currentPrincipalAccessor.Principal.AddIdentity(new ClaimsIdentity(mapClaims.Select(p => new Claim(map[p.Type], p.Value, p.ValueType, p.Issuer))));

                await next();
            });

            app.UseMultiTenancy();

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

            AsyncHelper.RunSync(async () =>
            {
                using (var scope = context.ServiceProvider.CreateScope())
                {
                    await scope.ServiceProvider
                        .GetRequiredService<IDataSeeder>()
                        .SeedAsync();
                }
            });
        }
    }
}
