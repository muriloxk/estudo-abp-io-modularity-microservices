using Volo.Abp.Authorization.Permissions;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;

namespace AbpMicroRabbit.Banking.Application.Contracts.Permissions
{
    public class BankingPermissionDefinitionProvider: PermissionDefinitionProvider
    {
        private readonly IPermissionManager _permissionManager;


        public BankingPermissionDefinitionProvider(IPermissionManager permissionManager)
        {
            _permissionManager = permissionManager;
        }

        public override void Define(IPermissionDefinitionContext context)
        {
            var bankingGroup = context.AddGroup(BankingPermissions.GroupName);

            bankingGroup.AddPermission(BankingPermissions.Accounts.Default, null, MultiTenancySides.Tenant)
                          .AddChild(BankingPermissions.Accounts.Create, null, MultiTenancySides.Tenant)
                          .AddChild(BankingPermissions.Accounts.Update, null, MultiTenancySides.Tenant)
                          .AddChild(BankingPermissions.Accounts.Delete, null, MultiTenancySides.Tenant)
                          .AddChild(BankingPermissions.Accounts.Transfer, null, MultiTenancySides.Tenant)
                          .WithProviders(RolePermissionValueProvider.ProviderName);
        }
    }
}
