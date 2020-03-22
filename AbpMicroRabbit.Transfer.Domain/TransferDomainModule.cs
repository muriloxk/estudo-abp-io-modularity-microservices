using AbpMicroRabbit.Shared.Domain;
using Volo.Abp.Modularity;

namespace AbpMicroRabbit.Transfer.Domain
{
    [DependsOn(typeof(AbpMicroRabbitSharedDomainModule))]
    public class TransferDomainModule : AbpModule
    {

    }
}
