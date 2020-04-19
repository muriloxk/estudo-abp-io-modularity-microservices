using Volo.Abp.Modularity;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.Autofac;
using Volo.Abp.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AuthenticationServer.EntityFramework;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.Threading;
using Volo.Abp.Data;
using Volo.Abp.PermissionManagement.IdentityServer;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Account.Web;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.Auditing;

namespace AuthenticationServer
{
    [DependsOn(typeof(AbpAutofacModule),
                typeof(AbpAuditingModule),
                typeof(AbpIdentityAspNetCoreModule),
                typeof(AbpPermissionManagementEntityFrameworkCoreModule),
                typeof(AbpSettingManagementEntityFrameworkCoreModule),
                typeof(AbpIdentityEntityFrameworkCoreModule),
                typeof(AbpAccountApplicationModule),
                typeof(AbpAccountWebModule),
                typeof(AbpIdentityServerEntityFrameworkCoreModule),
                typeof(AbpEntityFrameworkCoreMySQLModule),
                typeof(AbpAccountWebIdentityServerModule),
                typeof(AbpAspNetCoreMvcUiBasicThemeModule),
                typeof(AbpAspNetCoreMvcUiThemeSharedModule),
                typeof(AbpPermissionManagementDomainIdentityServerModule),
                typeof(AbpMultiTenancyModule),
                typeof(AbpAspNetCoreMultiTenancyModule),
                typeof(AbpTenantManagementEntityFrameworkCoreModule),
                typeof(AbpTenantManagementApplicationContractsModule))]
    public class AuthenticationServerModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddCors(options => options.AddDefaultPolicy(builder =>
                                                                                    builder.AllowAnyOrigin()
                                                                                           .AllowAnyHeader()
                                                                                           .AllowAnyMethod()));
            context.Services.AddControllersWithViews();
            context.Services.AddAbpDbContext<AuthServerDbContext>(options =>
            {
                options.AddDefaultRepositories();
            });

            Configure<AbpAuditingOptions>(options =>
            {
                options.EntityHistorySelectors.AddAllEntities();
                options.ApplicationName = "Banking Service";
                options.IsEnabledForGetRequests = true;
            });

            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = true;
            });

            Configure<AbpTenantResolveOptions>(options =>
            {
                options.TenantResolvers.Insert(1, new HeaderTenantResolveContributor());
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL(options => options.ServerVersion(new Version(8, 0, 19), ServerType.MySql));
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseVirtualFiles();

            app.UseRouting();

            app.UseCors();

            app.UseStaticFiles();

            app.UseMultiTenancy();
      
            app.UseIdentityServer();

            app.UseMvcWithDefaultRouteAndArea();

            app.UseAuditing();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

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