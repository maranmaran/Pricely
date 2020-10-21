using Autofac;
using EventBus.Azure.Interfaces;
using EventBus.Infrastructure;
using EventBus.Infrastructure.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EventBus.Azure
{
    public static class Setup
    {
        public static void ConfigureAzureEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            // Connection 
            services.AddSingleton<IPersistentConnection>(sp =>
            {
                var serviceBusConnectionString = configuration.GetValue<string>("EventBus:AzureConnection");
                var serviceBusConnection = new ServiceBusConnectionStringBuilder(serviceBusConnectionString);

                return new DefaultPersistentConnection(serviceBusConnection);
            });

            // Subscription manager
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            // Event bus
            var subscriptionClientName = configuration.GetValue<string>("EventBus:SubscriptionClientName");
            services.AddSingleton<IEventBus, EventBus>(sp =>
            {
                var persistentConnection = sp.GetRequiredService<IPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBus>>();
                var eventBusSubscriptionManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                return new EventBus(persistentConnection, logger,
                    eventBusSubscriptionManager, subscriptionClientName, iLifetimeScope);
            });
        }
    }
}
