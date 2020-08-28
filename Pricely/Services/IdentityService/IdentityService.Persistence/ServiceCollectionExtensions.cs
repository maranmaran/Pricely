using IdentityService.Domain;
using IdentityService.Domain.Entities;
using IdentityService.Persistence.Interfaces;
using IdentityService.Persistence.Repositories;
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
            services.AddDbContext<ApplicationDbContext>(o =>
            {
                o.UseSqlServer(configuration.GetSection(nameof(DatabaseSettings))["ConnectionString"]);
                o.EnableSensitiveDataLogging();
                o.EnableDetailedErrors();
            });

            // Add identity
            services.AddIdentityCore<Company>(o =>
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
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            //services.AddTransient<IPokemonRepository, PokemonRepository>();
        }


    }
}
