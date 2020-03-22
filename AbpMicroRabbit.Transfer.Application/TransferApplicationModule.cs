using AbpMicroRabbit.Transfer.Application.Contracts;
using AbpMicroRabbit.Transfer.Domain;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace AbpMicroRabbit.Transfer.Application
{
    [DependsOn(typeof(TransferApplicationContractsModule),
               typeof(TransferDomainModule),
               typeof(AbpAutoMapperModule))]
    public class TransferApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<TransferApplicationAutoMapperProfile>();
            });

            context.Services.AddAutoMapperObjectMapper<TransferApplicationModule>();
        }
    }
}
