using Volo.Abp.Modularity;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.Autofac;

namespace AuthenticationServer
{
    [DependsOn(typeof(AbpIdentityServerEntityFrameworkCoreModule))]
    [DependsOn(typeof(AbpMultiTenancyModule))]
    [DependsOn(typeof(AbpEntityFrameworkCoreMySQLModule))]
    [DependsOn(typeof(AbpAspNetCoreModule))]
    [DependsOn(typeof(AbpAspNetCoreMultiTenancyModule))]
    [DependsOn(typeof(AbpAutofacModule))]
    public class AuthenticationServerModule : AbpModule
    {
        // Vamos fazer a configuração do nosso querido e amado IDENTITY SERVER.

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = true;
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL();
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseRouting();
            app.UseMultiTenancy();
            app.UseIdentityServer();
        }
    }
}