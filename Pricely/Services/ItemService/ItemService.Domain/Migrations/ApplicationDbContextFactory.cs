using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ItemService.Domain.Migrations
{
    /// <summary>
    /// Used for Ef CLI tool to create db context
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // IDesignTimeDbContextFactory is used usually when you execute EF Core commands like Add-Migration, Update-Database, and so on
            Console.WriteLine("Which environment you wish to operate with");
            var environment = (Console.ReadLine())?.ToLower().Trim();

            if (environment != "dev" && environment != "prod")
                throw new Exception("Environment can only be Development or Release");

            if (environment == "dev") environment = "Development";
            if (environment == "prod") environment = "Release";

            // Build config
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ItemService.API"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            var connectionString = config.GetSection("DatabaseSettings")["ConnectionString"];

            // Here we create the DbContextOptionsBuilder manually.        
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(connectionString);

            // Create our DbContext.
            return new ApplicationDbContext(builder.Options);
        }
    }
}