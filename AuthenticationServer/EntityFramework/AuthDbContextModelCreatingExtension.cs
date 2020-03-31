using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;

namespace AuthenticationServer.EntityFramework
{
    public static class AuthDbContextModelCreatingExtension
    {
        public static void SeedApiResources(this ModelBuilder builder)
        {
            //TODO: Caso necessitar demais claims para o estudo criar um commom claims como no AuthServerSeed do sample.
            var bankingService = new ApiResource(Guid.NewGuid(), "BankingService", "BankingServiceApi");
            bankingService.AddUserClaim("role");

            var transferLogService = new ApiResource(Guid.NewGuid(), "TransferLogService", "TransferLogServiceApi");
            transferLogService.AddUserClaim("role");
          

            builder.Entity<ApiResource>(b =>
            {
                b.HasData(
                          bankingService,
                          transferLogService
                         );
            });
        }


        public static void SeedClientResources(this ModelBuilder builder)
        {
            builder.Entity<Client>(b =>
            {
                //TODO: Cadastrar futuros clients
            });
        }

        public static void SeedIdentityResources(this ModelBuilder builder)
        {
            var resources = new[]
            {
                new IdentityServer4.Models.IdentityResources.OpenId(),
                new IdentityServer4.Models.IdentityResources.Profile(),
                new IdentityServer4.Models.IdentityResources.Email(),
                new IdentityServer4.Models.IdentityResources.Address(),
                new IdentityServer4.Models.IdentityResources.Phone(),
                new IdentityServer4.Models.IdentityResource("role", "Roles of the user", new[] {"role"})
            };

            

            foreach (var resource in resources)
                builder.Entity<IdentityClaimType>(b =>
                {
                    foreach (string claim in resource.UserClaims)
                    {
                        b.HasData(new IdentityClaimType(Guid.NewGuid(), claim, isStatic: true));
                    }
                });

            foreach (var resource in resources)
            {
                builder.Entity<IdentityResource>(b =>
                {
                    b.HasData(new IdentityResource(Guid.NewGuid(), resource));
                });
            }
        }
    }
}
