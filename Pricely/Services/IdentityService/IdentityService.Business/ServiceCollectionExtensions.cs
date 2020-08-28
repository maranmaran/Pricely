using IdentityService.Business.Interfaces;
using IdentityService.Business.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IdentityService.Business
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureBusinessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // configure database settings
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            services.AddSingleton(x => x.GetService<IOptions<JwtSettings>>().Value);

            services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
        }

    }
}
