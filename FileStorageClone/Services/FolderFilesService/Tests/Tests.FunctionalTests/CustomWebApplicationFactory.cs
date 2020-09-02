using FolderFilesService.Business.Settings;
using FolderFilesService.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.FunctionalTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public ServiceProvider ServiceProvider { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context using an in-memory 
                // database for testing.
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                services.AddSingleton<AppSettings>();


                // Build the service provider.
                ServiceProvider = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context
                using var scope = ServiceProvider.CreateScope();

                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ApplicationDbContext>();

                var concreteContext = context;

                // Ensure the database is created.
                concreteContext.Database.EnsureCreated();
                concreteContext.Database.Migrate();

                NLog.LogManager.DisableLogging();
            });
        }
    }
}
