using Volo.Abp.Modularity;
using Volo.Abp.Identity;

namespace AbpMicroRabbit.Banking.Application.Contracts
{
    [DependsOn(typeof(AbpIdentityApplicationContractsModule))]
    public class BankingApplicationContractsModule : AbpModule
    {
       
    }
}
