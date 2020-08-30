using MenuService.Persistence.Interfaces;
using MenuService.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MenuService.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigurePersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // configure database settings
            services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton(x => x.GetService<IOptions<DatabaseSettings>>().Value);

            // Add database

            // Configure context for DI
            services.AddTransient<ApplicationDbContext>();

            // add repositories to DI
            services.AddTransient(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            //services.AddTransient<IPokemonRepository, PokemonRepository>();
        }


    }
}
