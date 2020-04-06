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
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.PermissionManagement.IdentityServer;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Account.Web;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;

namespace AuthenticationServer
{

    [DependsOn(typeof(AbpAutofacModule),
               typeof(AbpAspNetCoreMvcModule),
               typeof(AbpIdentityAspNetCoreModule),
               typeof(AbpPermissionManagementEntityFrameworkCoreModule),
               typeof(AbpSettingManagementEntityFrameworkCoreModule),
               typeof(AbpIdentityEntityFrameworkCoreModule),
               typeof(AbpAccountApplicationModule),
               typeof(AbpAccountWebModule), //testar interface 
               typeof(AbpIdentityServerEntityFrameworkCoreModule),
               typeof(AbpEntityFrameworkCoreMySQLModule),
               typeof(AbpAccountWebIdentityServerModule),
               typeof(AbpAspNetCoreMvcUiBasicThemeModule),
               typeof(AbpPermissionManagementDomainIdentityServerModule),
               typeof(AbpMultiTenancyModule),
               typeof(AbpAspNetCoreMultiTenancyModule)
   )]
    public class AuthenticationServerModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin()
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

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL();
            });


            //context.Services.AddAuthentication(o =>
            //{
            //    o.DefaultAuthenticateScheme = IdentityServerConstants.DefaultCookieAuthenticationScheme;
            //    o.DefaultSignOutScheme = IdentityServerConstants.SignoutScheme;
            //});

        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseCors();
            app.UseRouting();
            app.UseStaticFiles();

            //app.UseAuthentication();
            app.UseIdentityServer();

            //app.UseMvc();
            app.UseMvcWithDefaultRouteAndArea();

            //app.UseMultiTenancy();

            //app.UseAuthorization();

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