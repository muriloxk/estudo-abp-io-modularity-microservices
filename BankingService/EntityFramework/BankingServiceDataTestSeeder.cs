using System;
using System.Threading.Tasks;
using AbpMicroRabbit.Banking.Application.Contracts.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace BankingService.EntityFramework
{
    public class BankingServiceDataTestSeeder: IDataSeedContributor, ITransientDependency
    {
        private readonly IPermissionManager _permissionManager;
        private readonly ICurrentTenant _currentTenant;
        private readonly IPermissionGrantRepository _permissionGrantRepository;

        public BankingServiceDataTestSeeder(IPermissionManager permissionManager,
                                        IPermissionGrantRepository permissionGrantRepository,
                                        ICurrentTenant currentTenant)
        {
            _currentTenant = currentTenant;
            _permissionManager = permissionManager;
            _permissionGrantRepository = permissionGrantRepository;     
        }

        [UnitOfWork]
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            _currentTenant.Change(new Guid("497f79b8-1699-5764-2b8e-39f465435497"));

            await seedPermissoesAdmin();
            await seedPermissoesAssistente();
        }

        private async Task seedPermissoesAdmin()
        {
            var role = "admin";

            if (!await VerificarSePermissionParaORoleJaEstaCadastrada(role, BankingPermissions.Accounts.Default))
                await _permissionManager.SetForRoleAsync(role, BankingPermissions.Accounts.Default, true);

            if (!await VerificarSePermissionParaORoleJaEstaCadastrada(role, BankingPermissions.Accounts.Create))
                await _permissionManager.SetForRoleAsync(role, BankingPermissions.Accounts.Create, true);

            if (!await VerificarSePermissionParaORoleJaEstaCadastrada(role, BankingPermissions.Accounts.Update))
                await _permissionManager.SetForRoleAsync(role, BankingPermissions.Accounts.Update, true);

            if (!await VerificarSePermissionParaORoleJaEstaCadastrada(role, BankingPermissions.Accounts.Delete))
                await _permissionManager.SetForRoleAsync(role, BankingPermissions.Accounts.Delete, true);

            if (!await VerificarSePermissionParaORoleJaEstaCadastrada(role, BankingPermissions.Accounts.Transfer))
                await _permissionManager.SetForRoleAsync(role, BankingPermissions.Accounts.Transfer, true);
        }

        private async Task seedPermissoesAssistente()
        {
            var role = "assistente";

            if (!await VerificarSePermissionParaORoleJaEstaCadastrada(role, BankingPermissions.Accounts.Default))
                await _permissionManager.SetForRoleAsync(role, BankingPermissions.Accounts.Default, true);

            if (!await VerificarSePermissionParaORoleJaEstaCadastrada(role, BankingPermissions.Accounts.Create))
                await _permissionManager.SetForRoleAsync(role, BankingPermissions.Accounts.Create, false);

            if (!await VerificarSePermissionParaORoleJaEstaCadastrada(role, BankingPermissions.Accounts.Create))
                await _permissionManager.SetForRoleAsync(role, BankingPermissions.Accounts.Create, false);

            if (!await VerificarSePermissionParaORoleJaEstaCadastrada(role, BankingPermissions.Accounts.Delete))
                await _permissionManager.SetForRoleAsync(role, BankingPermissions.Accounts.Delete, false);

            if (!await VerificarSePermissionParaORoleJaEstaCadastrada(role, BankingPermissions.Accounts.Transfer))
                await _permissionManager.SetForRoleAsync(role, BankingPermissions.Accounts.Transfer, false);
        }

        private async Task<bool> VerificarSePermissionParaORoleJaEstaCadastrada(string providerKey, string permission)
        {
            return await _permissionGrantRepository.FindAsync(permission, RolePermissionValueProvider.ProviderName, providerKey) != null;
        }
    }
}
