using Autofac.Extensions.DependencyInjection;
using MenuService.Persistence.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MongoDatabaseSettings = DataAccess.NoSql.Settings.MongoDatabaseSettings;

namespace Tests.MenuService.Functional.Infrastructure
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
            var configuration = services.GetService<IConfiguration>();

            var connString =
                configuration.GetValue<string>(
                    $"{nameof(MongoDatabaseSettings)}:{nameof(MongoDatabaseSettings.ConnectionString)}");

            var client = new MongoClient(connString);

            var databaseName =
                configuration.GetValue<string>(
                    $"{nameof(MongoDatabaseSettings)}:{nameof(MongoDatabaseSettings.Database)}");

            // ReSharper disable once MethodHasAsyncOverload
            client.DropDatabase(databaseName);

            DbSeeder.Seed(services);
        }
    }
}