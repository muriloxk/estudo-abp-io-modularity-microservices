using AbpMicroRabbit.Shared.Domain;
using AbpMicroRabbit.Shared.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace AbpMicroRabbit.Banking.Domain
{
    [DependsOn(typeof(AbpMicroRabbitSharedDomainModule),
               typeof(AbpMicroRabbitSharedInfraBusModule))]
    public class BankingDomainModule : AbpModule
    {
       
    }   
}
