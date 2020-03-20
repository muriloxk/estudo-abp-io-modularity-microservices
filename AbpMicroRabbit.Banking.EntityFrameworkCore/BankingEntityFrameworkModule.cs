using AbpMicroRabbit.Banking.Domain;
using AbpMicroRabbit.Banking.EntityFrameworkCore.Context;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace AbpMicroRabbit.Banking.EntityFrameworkCore
{
    [DependsOn(typeof(BankingDomainModule))]
    public class BankingEntityFrameworkModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<BankingDbContext>(options =>
            {
                options.AddDefaultRepositories();
            });
        }
    }
}
