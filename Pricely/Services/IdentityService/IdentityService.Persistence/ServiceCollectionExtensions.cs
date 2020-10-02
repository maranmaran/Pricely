using DataAccess.Sql;
using DataAccess.Sql.Interfaces;
using IdentityService.Domain;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IdentityService.Persistence
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
                o.UseSqlServer(configuration.GetSection(nameof(DatabaseSettings))["ConnectionString"]);
                o.EnableSensitiveDataLogging();
                o.EnableDetailedErrors();
            });

            // Add identity
            services.AddIdentity<Company, Role>(o =>
            {
                o.Password = new PasswordOptions()
                {
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false,
                    RequiredLength = 2,
                    RequiredUniqueChars = 0
                };

                o.SignIn = new SignInOptions()
                {
                    //RequireConfirmedEmail = 
                };

            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

            // Configure context for DI
            services.AddTransient<ApplicationDbContext>();

            // add repositories to DI
            services.ConfigureEfSqlDataAccess();

            //services.AddTransient<IPokemonRepository, PokemonRepository>();
        }


    }
}
