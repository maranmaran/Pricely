using DataAccess.NoSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MenuService.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigurePersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // add repositories to DI
            services.ConfigureMongoDataAccess(configuration);
        }


    }
}
