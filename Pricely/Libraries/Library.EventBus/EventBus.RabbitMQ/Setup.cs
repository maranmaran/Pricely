using Autofac;
using EventBus.Infrastructure;
using EventBus.Infrastructure.Interfaces;
using EventBus.RabbitMQ.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace EventBus.RabbitMQ
{
    public static class Setup
    {
        public static void ConfigureRabbitMQEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var subscriptionClientName = configuration.GetValue<string>("EventBus:SubscriptionClientName");

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

            services.AddSingleton<IEventBus, EventBus>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<EventBus>>();

                var persistentConnection = sp.GetRequiredService<IPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var eventBusSubscriptionManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = configuration.GetValue<int>("EventBus:RetryCount");

                return new EventBus(logger, persistentConnection, eventBusSubscriptionManager, iLifetimeScope, subscriptionClientName, retryCount);
            });
        }
    }
}
