using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.Autofac;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace IdentityService
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpAspNetCoreMultiTenancyModule),
        typeof(AbpTenantManagementEntityFrameworkCoreModule))]
    public class IdentityServiceModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = true;
            });

            context.Services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.ApiName = configuration["AuthServer:ApiName"];
                    options.RequireHttpsMetadata = false;
                });

            context.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Identity Service", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });


            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL();
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseCorrelationId();
            app.UseRouting();
            app.UseAuthentication();

            app.UseMultiTenancy();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity Service");
            });
        }
    }
}
