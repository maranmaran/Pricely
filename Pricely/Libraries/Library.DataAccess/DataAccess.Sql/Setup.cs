using DataAccess.Sql.Interfaces;
using DataAccess.Sql.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Sql
{
    public static class Setup
    {
        public static void ConfigureEfSqlDataAccess(this IServiceCollection services)
        {
            // add repositories to DI
            services.AddTransient(typeof(IGenericEfRepository<>), typeof(GenericEfRepository<>));
        }
    }
}
