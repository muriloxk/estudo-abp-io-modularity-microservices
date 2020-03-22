using AbpMicroRabbit.Transfer.Domain;
using AbpMicroRabbit.Transfer.EntityFrameworkCore.Context;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace AbpMicroRabbit.Transfer.EntityFrameworkCore
{
    [DependsOn(typeof(AbpEntityFrameworkCoreModule),
               typeof(TransferDomainModule))]
    public class TransferEntityFrameworkModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAssembly(typeof(TransferEntityFrameworkModule).Assembly);

            context.Services.AddAbpDbContext<TransferDbContext>(options =>
            {
                options.AddDefaultRepositories();
            });
        }
    }
}
