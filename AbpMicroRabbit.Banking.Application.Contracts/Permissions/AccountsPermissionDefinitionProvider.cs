using Volo.Abp.Authorization.Permissions;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;

namespace AbpMicroRabbit.Banking.Application.Contracts.Permissions
{
    public class AccountsPermissionDefinitionProvider: PermissionDefinitionProvider
    {
        private readonly IPermissionManager _permissionManager;

        public AccountsPermissionDefinitionProvider(IPermissionManager permissionManager)
        {
            _permissionManager = permissionManager;
        }

        public override void Define(IPermissionDefinitionContext context)
        {
            var bankingGroup = context.AddGroup(AccountPermissions.GroupName);

            bankingGroup.AddPermission(AccountPermissions.Accounts.Default, null, MultiTenancySides.Tenant)
                        .AddChild(AccountPermissions.Accounts.Create, null, MultiTenancySides.Tenant)
                        .AddChild(AccountPermissions.Accounts.Update, null, MultiTenancySides.Tenant)
                        .AddChild(AccountPermissions.Accounts.Delete, null, MultiTenancySides.Tenant);


            _permissionManager.SetForRoleAsync("admin", AccountPermissions.Accounts.Default, true);
            _permissionManager.SetForRoleAsync("admin", AccountPermissions.Accounts.Create, true);
            _permissionManager.SetForRoleAsync("admin", AccountPermissions.Accounts.Update, true);
            _permissionManager.SetForRoleAsync("admin", AccountPermissions.Accounts.Delete, true);
            _permissionManager.SetForRoleAsync("admin", AccountPermissions.Accounts.Transfer, true);

            _permissionManager.SetForRoleAsync("assistente", AccountPermissions.Accounts.Default, true);
            _permissionManager.SetForRoleAsync("assistente", AccountPermissions.Accounts.Create, false);
            _permissionManager.SetForRoleAsync("assistente", AccountPermissions.Accounts.Update, false);
            _permissionManager.SetForRoleAsync("assistente", AccountPermissions.Accounts.Delete, false);
            _permissionManager.SetForRoleAsync("assistente", AccountPermissions.Accounts.Transfer, false);
        }
    }
}
