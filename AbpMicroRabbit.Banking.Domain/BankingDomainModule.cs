using AbpMicroRabbit.Shared.Domain;
using Volo.Abp.Modularity;

namespace AbpMicroRabbit.Banking.Domain
{
    [DependsOn(typeof(AbpMicroRabbitSharedDomainModule))]
    public class BankingDomainModule : AbpModule
    {
        
    }
}
