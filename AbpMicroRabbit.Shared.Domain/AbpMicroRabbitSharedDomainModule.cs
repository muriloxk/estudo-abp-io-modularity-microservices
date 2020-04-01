using Volo.Abp.Modularity;
using Volo.Abp.Identity;

namespace AbpMicroRabbit.Shared.Domain
{
    [DependsOn(typeof(AbpIdentityDomainModule))]
    public class AbpMicroRabbitSharedDomainModule : AbpModule
    {
        
    }
}
