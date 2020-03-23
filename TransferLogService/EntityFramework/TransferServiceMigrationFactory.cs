using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TransferLogService.EntityFramework
{
    public class TransferServiceMigrationFactory : IDesignTimeDbContextFactory<TransferMigrationsDbContext>
    {
        public TransferMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false).Build();

            var optionsBuilder = new DbContextOptionsBuilder<TransferMigrationsDbContext>();
            optionsBuilder.UseMySql(configuration.GetConnectionString("TransferDb"));

            return new TransferMigrationsDbContext(optionsBuilder.Options);
        }
    }
}
