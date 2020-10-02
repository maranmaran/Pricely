using DataAccess.NoSql.Interfaces;
using DataAccess.NoSql.Repositories;
using DataAccess.NoSql.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DataAccess.NoSql
{
    public static class Setup
    {
        public static void ConfigureMongoDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDatabaseSettings>(configuration.GetSection(nameof(MongoDatabaseSettings)));
            services.AddSingleton(x => x.GetService<IOptions<MongoDatabaseSettings>>().Value);

            // add repositories to DI
            services.AddTransient(typeof(IGenericDocumentRepository<>), typeof(MongoRepository<>));
        }
    }
}
