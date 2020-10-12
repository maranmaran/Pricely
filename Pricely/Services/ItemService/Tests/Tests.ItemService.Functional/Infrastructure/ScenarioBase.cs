using Autofac.Extensions.DependencyInjection;
using DataAccess.Sql.Interfaces;
using ItemService.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Tests.ItemService.Functional.Infrastructure
{
    /// <summary>
    /// Base class to setup clients for integration testing
    /// </summary>
    public abstract class ScenarioBase
    {
        /// <summary>
        /// Creates generic test host using custom appsettings and configuration setup
        /// </summary>
        /// <returns>Generic host for testing</returns>
        public async Task<IHost> CreateHost()
        {
            var path = Assembly.GetAssembly(typeof(TestStartup))?.Location;

            var hostBuilder = new HostBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHost(webHostBuilder =>
                {
                    // configure test web host which will be used to create test server
                    webHostBuilder
                        .UseContentRoot(Path.GetDirectoryName(path))
                        .ConfigureAppConfiguration(config =>
                        {
                            config
                                .AddJsonFile("appsettings.json")
                                .AddEnvironmentVariables();
                        })
                        .ConfigureServices((builder, services) =>
                        {
                            var provider = services
                                .AddEntityFrameworkInMemoryDatabase()
                                .BuildServiceProvider();

                            var optionsBuilder =
                                new DbContextOptionsBuilder<ApplicationDbContext>(
                                    new DbContextOptions<ApplicationDbContext>(new Dictionary<Type, IDbContextOptionsExtension>())
                                )
                                .UseInMemoryDatabase("ItemServiceIntegrationTests")
                                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                                .UseInternalServiceProvider(provider);

                            services.AddScoped(x => optionsBuilder.Options);
                        })
                        .UseStartup<TestStartup>()
                        .UseTestServer();
                });

            var host = await hostBuilder.StartAsync();

            // setup stuff
            // db migrations etc..
            // ReSharper disable once ConvertToUsingDeclaration
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                ReseedDatabase(services);
            }

            return host;
        }

        /// <summary>
        /// Ensures db is deleted and recreated / seeded
        /// </summary>
        /// <param name="services"></param>
        private void ReseedDatabase(IServiceProvider services)
        {
            var dbContext = services.GetService<IApplicationDbContext>();

            dbContext.Database.EnsureDeleted();

            dbContext.Database.EnsureCreated();
        }
    }
}