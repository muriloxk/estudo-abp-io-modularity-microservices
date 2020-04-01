using AbpMicroRabbit.Banking.Application.Contracts;
using AbpMicroRabbit.Banking.Domain;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Identity;

namespace AbpMicroRabbit.Banking.Application
{
    [DependsOn(typeof(BankingDomainModule),
               typeof(BankingApplicationContractsModule),
               typeof(AbpAutoMapperModule))]
 
    public class BankingApplicationModule : AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<BankingApplicationAutoMapperProfile>();
            });

            context.Services.AddAutoMapperObjectMapper<BankingApplicationModule>();
        }
    }
}
