using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.Autofac;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace TenantService
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpTenantManagementHttpApiModule),
        typeof(AbpTenantManagementEntityFrameworkCoreModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpAspNetCoreMultiTenancyModule),
        typeof(AbpIdentityEntityFrameworkCoreModule)
        )]
    public class TenantServiceModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = true;
            });

            Configure<AbpTenantResolveOptions>(options =>
            {
                options.TenantResolvers.Insert(1, new HeaderTenantResolveContributor());
            });

            context.Services.AddAuthentication("Bearer")
                            .AddIdentityServerAuthentication(options =>
                            {
                                options.Authority = configuration["AuthServer:Authority"];
                                options.ApiName = configuration["AuthServer:ApiName"];
                                options.RequireHttpsMetadata = false;
                                options.LegacyAudienceValidation = true;
                            });

            context.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Tenant Management Service API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });


            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL();
            });

            //Teste
            context.Services.AddAlwaysAllowAuthorization();
        }


        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseRouting();
            app.UseAuthentication();
            app.UseMultiTenancy();

            app.UseAbpRequestLocalization(); //TODO: localization?
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Tenant Management Service API");
            });

            app.UseMvcWithDefaultRouteAndArea();
        }
    }
}