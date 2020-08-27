using EventBus.Infrastructure.Interfaces;
using ItemService.API.Middleware;
using ItemService.Business;
using ItemService.Business.Queries.Items.GetItems;
using ItemService.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ItemService.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // layers
            services.RegisterBusinessServices();
            services.ConfigurePersistenceLayer(Configuration);

            // external libraries
            services.ConfigureMvcWithFluentValidation();
            services.ConfigureMediatR();
            services.ConfigureSwagger();
            services.ConfigureLazyCache();
            services.ConfigureAutomapper();
            NLogBuilder.ConfigureNLog("nlog.config");

            //internal libs
            services.ConfigureEventBus(Configuration);

            // system configuration
            services.ConfigureResponseCompression(); // response compression
            services.ConfigureCors(Configuration); // Cors
            services.ConfigureHealthCheck();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowedCorsOrigins");


            // ===== Middleware to serve generated Swagger as a JSON endpoint and serve swagger-ui =====
            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Items API");
                setup.RoutePrefix = "api";
                setup.DocExpansion(DocExpansion.None);
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // ==== Response compression ====
            app.UseResponseCompression();

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("hc");
                endpoints.MapControllers();
            });


            // DEMO: subscribe handlers
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<HelloEvent, HelloEventHandler>();
        }
    }
}
