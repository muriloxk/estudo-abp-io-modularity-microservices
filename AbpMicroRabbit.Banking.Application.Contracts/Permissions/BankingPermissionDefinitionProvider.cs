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

          var x =  bankingGroup.AddPermission(BankingPermissions.Accounts.Default, null, MultiTenancySides.Tenant)
                        .AddChild(BankingPermissions.Accounts.Create, null, MultiTenancySides.Tenant)
                        .AddChild(BankingPermissions.Accounts.Update, null, MultiTenancySides.Tenant)
                        .AddChild(BankingPermissions.Accounts.Delete, null, MultiTenancySides.Tenant)
                        .WithProviders(RolePermissionValueProvider.ProviderName);


            //Todo: Mover um service ou dataseeder no host do Banking
            _permissionManager.SetForRoleAsync("admin", BankingPermissions.Accounts.Default, true);
            _permissionManager.SetForRoleAsync("admin", BankingPermissions.Accounts.Create, true);
            _permissionManager.SetForRoleAsync("admin", BankingPermissions.Accounts.Update, true);
            _permissionManager.SetForRoleAsync("admin", BankingPermissions.Accounts.Delete, true);
            _permissionManager.SetForRoleAsync("admin", BankingPermissions.Accounts.Transfer, true);

            _permissionManager.SetForRoleAsync("assistente", BankingPermissions.Accounts.Default, true);
            _permissionManager.SetForRoleAsync("assistente", BankingPermissions.Accounts.Create, false);
            _permissionManager.SetForRoleAsync("assistente", BankingPermissions.Accounts.Update, false);
            _permissionManager.SetForRoleAsync("assistente", BankingPermissions.Accounts.Delete, false);
            _permissionManager.SetForRoleAsync("assistente", BankingPermissions.Accounts.Transfer, false);
            
        }
    }
}
