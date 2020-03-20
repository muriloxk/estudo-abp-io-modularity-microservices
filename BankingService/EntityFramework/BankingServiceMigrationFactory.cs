using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BankingService.EntityFramework
{
    public class BankingServiceMigrationFactory : IDesignTimeDbContextFactory<BankingMigrationsDbContext>
    {
        public BankingMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false).Build();

            var optionsBuilder = new DbContextOptionsBuilder<BankingMigrationsDbContext>();
            optionsBuilder.UseMySql(configuration.GetConnectionString("BankingDb"));

            return new BankingMigrationsDbContext(optionsBuilder.Options); ;
        }
    }
}
