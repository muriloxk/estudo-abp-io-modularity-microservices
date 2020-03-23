using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TransferLogService.EntityFramework
{
    public class TransferServiceMigrationFactory
    {
        //public BankingMigrationsDbContext CreateDbContext(string[] args)
        //{
        //    var configuration = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json", optional: false).Build();

        //    var optionsBuilder = new DbContextOptionsBuilder<BankingMigrationsDbContext>();
        //    optionsBuilder.UseMySql(configuration.GetConnectionString("BankingDb"));

        //    return new BankingMigrationsDbContext(optionsBuilder.Options); 
        //}

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
