using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
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


            const string commonSecret = "E5Xd4yMqjP5kjWFKrYgySBju6JVfCzMyFp7n2QmMrME=";

            var commonScopes = new[]
            {
                "email",
                "openid",
                "profile",
                "role",
                "phone",
                "address"
            };


            await CreateClientAsync(
           "console-client-demo",
           new[] { "BloggingService", "IdentityService", "InternalGateway", "ProductService", "TenantManagementService" },
           new[] { "client_credentials", "password" },
           commonSecret );
        }


        private async Task<Client> CreateClientAsync(
      string name,
      IEnumerable<string> scopes,
      IEnumerable<string> grantTypes,
      string secret,
      string redirectUri = null,
      string postLogoutRedirectUri = null,
      IEnumerable<string> permissions = null)
        {
            var client = await _clientRepository.FindByCliendIdAsync(name);
            if (client == null)
            {
                client = await _clientRepository.InsertAsync(
                    new Client(
                        _guidGenerator.Create(),
                        name
                    )
                    {
                        ClientName = name,
                        ProtocolType = "oidc",
                        Description = name,
                        AlwaysIncludeUserClaimsInIdToken = true,
                        AllowOfflineAccess = true,
                        AbsoluteRefreshTokenLifetime = 31536000, //365 days
                        AccessTokenLifetime = 31536000, //365 days
                        AuthorizationCodeLifetime = 300,
                        IdentityTokenLifetime = 300,
                        RequireConsent = false
                    },
                    autoSave: true
                );
            }

            foreach (var scope in scopes)
            {
                if (client.FindScope(scope) == null)
                {
                    client.AddScope(scope);
                }
            }

            foreach (var grantType in grantTypes)
            {
                if (client.FindGrantType(grantType) == null)
                {
                    client.AddGrantType(grantType);
                }
            }

            if (client.FindSecret(secret) == null)
            {
                client.AddSecret(secret);
            }

            if (redirectUri != null)
            {
                if (client.FindRedirectUri(redirectUri) == null)
                {
                    client.AddRedirectUri(redirectUri);
                }
            }

            if (postLogoutRedirectUri != null)
            {
                if (client.FindPostLogoutRedirectUri(postLogoutRedirectUri) == null)
                {
                    client.AddPostLogoutRedirectUri(postLogoutRedirectUri);
                }
            }

            if (permissions != null)
            {
                await _permissionDataSeeder.SeedAsync(
                    ClientPermissionValueProvider.ProviderName,
                    name,
                    permissions
                );
            }

            return await _clientRepository.UpdateAsync(client);
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
