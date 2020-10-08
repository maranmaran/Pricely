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
            // If extended use Options to determine for which provider to add repository

            #region Entity Framework Core repositories

            // regular 
            services.AddTransient(typeof(IGenericEfRepository<>), typeof(GenericEfRepository<>));

            // uses projection - relies on automapper dependency
            services.AddTransient(typeof(IGenericEfRepository<,>), typeof(GenericEfRepository<,>));

            #endregion
        }
    }
}
