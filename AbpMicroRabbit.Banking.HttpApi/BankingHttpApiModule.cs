using AbpMicroRabbit.Banking.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace AbpMicroRabbit.Banking.HttpApi
{
    [DependsOn(typeof(BankingApplicationContractsModule),
               typeof(AbpAspNetCoreMvcModule))]
    public class BankingHttpApiModule : AbpModule
    {

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(BankingHttpApiModule).Assembly);
            });
        }

    }
}
