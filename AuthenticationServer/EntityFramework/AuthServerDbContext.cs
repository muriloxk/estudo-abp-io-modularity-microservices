using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;

namespace AuthenticationServer.EntityFramework
{
    public class AuthServerDbContext : AbpDbContext<AuthServerDbContext>
    {
        public AuthServerDbContext(DbContextOptions<AuthServerDbContext> options)
                : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureIdentity();
            modelBuilder.ConfigureIdentityServer(options =>
            {
                options.DatabaseProvider = EfCoreDatabaseProvider.MySql;
            });
        }
    }
}
