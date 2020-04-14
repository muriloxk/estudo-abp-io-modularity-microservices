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

namespace AuthenticationServer
{
    [DependsOn(typeof(AbpAutofacModule),
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

        //private readonly ICurrentTenant _currentTenant;

        //public AuthenticationServerModule(ICurrentTenant currentTenant)
        //{
        //    _currentTenant = currentTenant;
        //}

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

            //app.Use(async (context, next) =>
            //{
            //    context.Request.Headers["__tenant"] = "de76993a-f5c5-dd94-9a26-39f48895efe5";

            //    await next.Invoke();
            //});

            //app.Use(async (context, next) =>
            //{
            //    var teste = context.Request.Headers["__tenant"];
            //    await next.Invoke();
            //});

            //app.Use(async (context, next) =>
            //{
            //    var cultureQuery = context.Request.Query["culture"];
            //    if (!string.IsNullOrWhiteSpace(cultureQuery))
            //    {
            //        var culture = new CultureInfo("en-US");

            //        CultureInfo.CurrentCulture = culture;
            //        CultureInfo.CurrentUICulture = culture;
            //    }

            //    await next.Invoke();
            //});



            app.UseMultiTenancy();
            //app.UseAuthentication();
            app.UseIdentityServer();



            //app.UseMvc();
            app.UseMvcWithDefaultRouteAndArea();


            //app.UseAuthorization();

   
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

           //var teste2 = _currentTenant.Id;

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