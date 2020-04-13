using Volo.Abp.Modularity;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;

namespace AbpMicroRabbit.Banking.Application.Contracts
{
    [DependsOn(typeof(AbpIdentityApplicationContractsModule))]
    [DependsOn(typeof(AbpPermissionManagementEntityFrameworkCoreModule))]
    public class BankingApplicationContractsModule : AbpModule
    {
       
    }
}
