using Autofac.Extensions.DependencyInjection;
using MenuService.Persistence.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

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
                        .ConfigureAppConfiguration(cb =>
                        {
                            cb.AddJsonFile("appsettings.json", optional: false)
                                .AddEnvironmentVariables();
                        })
                        .UseStartup<TestStartup>()
                        .UseTestServer();
                });

            var host = await hostBuilder.StartAsync();

            // setup stuff
            // db migrations etc..
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                DbSeeder.Seed(services);
            }

            return host;
        }
    }
}