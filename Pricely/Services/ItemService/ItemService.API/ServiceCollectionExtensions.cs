using Autofac;
using AutoMapper;
using EventBus.Infrastructure;
using EventBus.Infrastructure.Interfaces;
using EventBus.RabbitMQ;
using EventBus.RabbitMQ.Interfaces;
using FluentValidation.AspNetCore;
using ItemService.API.LibraryConfigurations.MediatR;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

namespace ItemService.API
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures MediatR for request pipeline
        /// With logging and validation middleware
        /// </summary>
        public static void ConfigureMediatR(this IServiceCollection services)
        {
            var assemblies = new[]
            {
                Assembly.GetAssembly(typeof(Business.Mappings)),
            };

            services.AddMediatR(assemblies.ToArray());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        }

        /// <summary>
        /// Configures swagger ( Swashbuckle Core ) open api implementation
        /// For documentation and testing
        /// </summary>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(action =>
            {
                action.SwaggerDoc("v1", new OpenApiInfo { Title = "Items API", Version = "1" });


                // for swagger comments
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                action.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerGenNewtonsoftSupport(); // explicit opt-in - needs to be placed after AddSwaggerGen()
        }

        /// <summary>
        /// Configures MVC, JSON options and fluent validators
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureMvcJsonFluentValidation(this IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblies(new[]
                {
                    Assembly.GetAssembly(typeof(Business.Mappings)),
                }))
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.AllowInputFormatterExceptionMessages = true;
                });
        }

        /// <summary>
        /// Configures response compression
        /// </summary>
        public static void ConfigureResponseCompression(this IServiceCollection services)
        {
            services.AddResponseCompression();
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
        }

        /// <summary>
        /// Configures core settings
        /// </summary>
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowedCorsOrigins",
                    builder => builder
                        .WithOrigins(configuration.GetSection("CORSAllowedOrigins").Get<string[]>())
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                );
            });
        }

        /// <summary>
        /// Configures lazy cache library
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureLazyCache(this IServiceCollection services)
        {
            services.AddLazyCache();
        }

        /// <summary>
        /// Adds health checks
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks();
        }

        /// <summary>
        /// Configures automapper on service level
        /// </summary>
        public static void ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper>(provider =>
            {
                var config = new MapperConfiguration(c =>
                {
                    c.AddProfile<Business.Mappings>();
                });

                return config.CreateMapper();
            });
        }

        /// <summary>
        /// Configures event bus
        /// </summary>
        public static void ConfigureEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var subscriptionClientName = configuration.GetValue<string>("EventBus:SubscriptionClientName");

            if (configuration.GetValue<bool>("EventBus:AzureServiceBusEnabled") == false) // azure not implemented
            {
                services.AddSingleton<IPersistentConnection>(sp =>
                {
                    var logger = sp.GetRequiredService<ILogger<DefaultPersistentConnection>>();

                    var factory = new ConnectionFactory()
                    {
                        HostName = configuration.GetValue<string>("EventBus:Host"),
                        Port = configuration.GetValue<int>("EventBus:Port"),
                        UserName = configuration.GetValue<string>("EventBus:Username"),
                        Password = configuration.GetValue<string>("EventBus:Password"),
                        DispatchConsumersAsync = true,
                    };

                    var retryCount = configuration.GetValue<int>("EventBus:RetryCount");

                    return new DefaultPersistentConnection(logger, factory, retryCount);
                });

                services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

                services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
                {
                    var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();

                    var persistentConnection = sp.GetRequiredService<IPersistentConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    var retryCount = configuration.GetValue<int>("EventBus:RetryCount");

                    return new EventBusRabbitMQ(logger, persistentConnection, eventBusSubcriptionsManager, iLifetimeScope, subscriptionClientName, retryCount);
                });
            }

            // handlers
            //services.AddTransient<OrderStartedIntegrationEventHandler>();
        }
    }
}
