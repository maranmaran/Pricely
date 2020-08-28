using Autofac;
using EventBus.Infrastructure;
using EventBus.Infrastructure.Interfaces;
using EventBus.RabbitMQ;
using EventBus.RabbitMQ.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace ItemService.Business
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureBusinessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureEventBus(configuration);
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
