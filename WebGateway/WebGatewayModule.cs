using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Volo.Abp;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace WebGateway
{
    [DependsOn(typeof(AbpAutofacModule))]
    [DependsOn(typeof(AbpAspNetCoreMvcModule))]
    [DependsOn(typeof(AbpEntityFrameworkCoreMySQLModule))]
    [DependsOn(typeof(AbpPermissionManagementEntityFrameworkCoreModule))]
    [DependsOn(typeof(AbpSettingManagementEntityFrameworkCoreModule))]
    [DependsOn(typeof(AbpAspNetCoreMultiTenancyModule))]
    public class WebGatewayModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin()
                                                                                      .AllowAnyHeader()
                                                                                      .AllowAnyMethod()));

            context.Services.AddAuthentication("Bearer")
                            .AddIdentityServerAuthentication(options =>
                            {
                                options.Authority = configuration["AuthServer:Authority"];
                                options.ApiName = configuration["AuthServer:ApiName"];
                                options.RequireHttpsMetadata = false;
                            });

            context.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "PublicWebSite Gateway API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });

            context.Services.AddOcelot(context.Services.GetConfiguration());
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseCors();
            app.UseRouting();
            app.UseAuthentication();

            //app.Use(async (ctx, next) =>
            //{
            //    var currentPrincipalAccessor = ctx.RequestServices.GetRequiredService<ICurrentPrincipalAccessor>();
            //    var map = new Dictionary<string, string>()
            //    {
            //        { "sub", AbpClaimTypes.UserId },
            //        { "role", AbpClaimTypes.Role },
            //        { "email", AbpClaimTypes.Email },
            //        //any other map
            //    };
            //    var mapClaims = currentPrincipalAccessor.Principal.Claims.Where(p => map.Keys.Contains(p.Type)).ToList();
            //    currentPrincipalAccessor.Principal.AddIdentity(new ClaimsIdentity(mapClaims.Select(p => new Claim(map[p.Type], p.Value, p.ValueType, p.Issuer))));
            //    await next();
            //});

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "PublicWebSite Gateway API");
            });

            ConfigurarRotasParaDocumentacaoEGerarProxys(app);

            app.UseOcelot().Wait();
        }

        private static void ConfigurarRotasParaDocumentacaoEGerarProxys(IApplicationBuilder app)
        {
            app.MapWhen(
                 ctx => ctx.Request.Path.ToString().StartsWith("/api/abp/") ||
                        ctx.Request.Path.ToString().StartsWith("/Abp/"),
                 app2 =>
                 {
                     app2.UseRouting();
                     app2.UseMvcWithDefaultRouteAndArea();
                 }
             );
        }
    }
}
