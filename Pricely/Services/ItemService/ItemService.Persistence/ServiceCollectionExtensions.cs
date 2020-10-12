using DataAccess.Sql;
using DataAccess.Sql.Interfaces;
using ItemService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ItemService.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigurePersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // configure database settings
            services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton(x => x.GetService<IOptions<DatabaseSettings>>().Value);

            // Add database
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(o =>
            {
                o.UseSqlServer(configuration.GetValue<string>($"{nameof(DatabaseSettings)}:{nameof(DatabaseSettings.ConnectionString)}"));
                o.EnableSensitiveDataLogging();
                o.EnableDetailedErrors();
                o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.ConfigureEfSqlDataAccess();
            //services.AddTransient<IPokemonRepository, PokemonRepository>();
        }


    }
}
