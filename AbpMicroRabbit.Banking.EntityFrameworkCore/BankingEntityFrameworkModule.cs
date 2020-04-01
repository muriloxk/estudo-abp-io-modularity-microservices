using AbpMicroRabbit.Banking.Domain;
using AbpMicroRabbit.Banking.EntityFrameworkCore.Context;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace AbpMicroRabbit.Banking.EntityFrameworkCore
{
    [DependsOn(typeof(AbpEntityFrameworkCoreModule),
               typeof(BankingDomainModule))]
    [DependsOn(typeof(AbpIdentityServerEntityFrameworkCoreModule))]
    [DependsOn(typeof(AbpIdentityEntityFrameworkCoreModule))]
    public class BankingEntityFrameworkModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAssembly(typeof(BankingEntityFrameworkModule).Assembly);

            context.Services.AddAbpDbContext<BankingDbContext>(options =>
            {
                options.AddDefaultRepositories();
            });
        }
    }
}
