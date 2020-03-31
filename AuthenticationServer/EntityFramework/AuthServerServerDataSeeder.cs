using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace AuthenticationServer.EntityFramework
{
    public class AuthServerServerDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IApiResourceRepository _apiResourceRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IIdentityResourceDataSeeder _identityResourceDataSeeder;
        private readonly IGuidGenerator  _guidGenerator;
        private readonly IPermissionDataSeeder _permissionDataSeeder;

        public AuthServerServerDataSeeder(IApiResourceRepository apiResourceRepository,
                                          IClientRepository clientRepository,
                                          IIdentityResourceDataSeeder identityResourceDataSeeder,
                                          IGuidGenerator guidGenerator,
                                          IPermissionDataSeeder permissionDataSeeder)
        {
            _apiResourceRepository = apiResourceRepository;
            _clientRepository = clientRepository;
            _identityResourceDataSeeder = identityResourceDataSeeder;
            _guidGenerator = guidGenerator;
            _permissionDataSeeder = permissionDataSeeder;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            await _identityResourceDataSeeder.CreateStandardResourcesAsync();
            await CreateApiResourcesAsync();
        }

        private async Task CreateApiResourcesAsync()
        {
            await CreateApiResourceAsync("BankingService", new[] { "role" });
            await CreateApiResourceAsync("TranferLogService", new[] { "role" });
        }

        private async Task<ApiResource> CreateApiResourceAsync(string name, IEnumerable<string> claims)
        {
            var apiResource = await _apiResourceRepository.FindByNameAsync(name);

            if(apiResource == null)
            {
                apiResource = await _apiResourceRepository.InsertAsync(
                     new ApiResource( _guidGenerator.Create(),
                                      name,
                                      name + " API"), autoSave: true);
            }

            foreach(var claim in claims)
            {
                if(apiResource.FindClaim(claim) == null)
                {
                    apiResource.AddUserClaim(claim);
                }
            }

            return await _apiResourceRepository.UpdateAsync(apiResource);
        }
    }
}
