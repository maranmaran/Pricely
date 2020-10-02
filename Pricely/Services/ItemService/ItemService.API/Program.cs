using Autofac.Extensions.DependencyInjection;
using DataAccess.Sql.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Reflection;

namespace ItemService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var logger = services.GetService<ILogger<Program>>();

                try
                {
                    MigrateDatabase(logger, services); // comment if you don't want seed values in migrations

                    logger.LogInformation($"Running {Assembly.GetExecutingAssembly().FullName}");
                    host.Run();
                }
                catch (Exception e)
                {
                    logger.LogError($"{Assembly.GetExecutingAssembly().FullName} startup failed {e.Message} {e.InnerException?.Message}");
                    throw;
                }
                finally
                {
                    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                    NLog.LogManager.Shutdown();
                }
            }
        }

        private static void MigrateDatabase(ILogger<Program> logger, IServiceProvider services)
        {
            logger.LogInformation("Migrating DB");

            var context = services.GetService<IApplicationDbContext>();
            context.Database.Migrate();

            logger.LogInformation("Finished migrating DB");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace); // App settings override this
                })
                .UseNLog();
    }
}
