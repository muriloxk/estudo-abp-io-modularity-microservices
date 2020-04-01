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
using Volo.Abp.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AuthenticationServer.EntityFramework;
using Volo.Abp.IdentityServer;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.Threading;
using Volo.Abp.Data;
using Volo.Abp.AspNetCore.Mvc;

namespace AuthenticationServer
{
    //[DependsOn(typeof(AbpAutofacModule))]
    //[DependsOn(typeof(AbpIdentityServerEntityFrameworkCoreModule))]
    //[DependsOn(typeof(AbpMultiTenancyModule))]
    //[DependsOn(typeof(AbpEntityFrameworkCoreMySQLModule))]
    //[DependsOn(typeof(AbpAspNetCoreModule))]
    //[DependsOn(typeof(AbpAspNetCoreMultiTenancyModule))]
    //[DependsOn(typeof(AbpAutofacModule))]
    //[DependsOn(typeof(AbpIdentityEntityFrameworkCoreModule))]
    //[DependsOn(typeof(AbpPermissionManagementEntityFrameworkCoreModule))]


    [DependsOn(typeof(AbpAutofacModule),
               typeof(AbpAspNetCoreMvcModule),
               typeof(AbpIdentityServerEntityFrameworkCoreModule),
               typeof(AbpMultiTenancyModule),
               typeof(AbpEntityFrameworkCoreMySQLModule),
               typeof(AbpAspNetCoreModule),
               typeof(AbpAspNetCoreMultiTenancyModule),
               typeof(AbpIdentityEntityFrameworkCoreModule),
               typeof(AbpIdentityServerDomainModule),
               typeof(AbpIdentityServerDomainSharedModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule))]
    public class AuthenticationServerModule : AbpModule
    {
        // Vamos fazer a configuração do nosso querido e amado IDENTITY SERVER.

        //public override void PreConfigureServices(ServiceConfigurationContext context)
        //{
        //    context.Services.PreConfigure<IIdentityServerBuilder>(
        //        builder =>
        //        {
        //            builder.AddAbpStores();
        //        });
        //}


        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAssembly(typeof(AuthenticationServerModule).Assembly);

            context.Services.AddAbpDbContext<AuthServerDbContext>(options =>
            {
                options.AddDefaultRepositories();
            });

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