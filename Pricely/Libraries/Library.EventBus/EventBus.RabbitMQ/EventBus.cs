using Autofac;
using EventBus.Infrastructure.Extensions;
using EventBus.Infrastructure.Interfaces;
using EventBus.Infrastructure.Models;
using EventBus.RabbitMQ.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.RabbitMQ
{
    public class EventBus : IEventBus, IDisposable
    {
        private const string BrokerName = "event_bus";
        private const string ScopeName = "event_bus";

        private readonly ILogger<EventBus> _logger;
        private readonly IPersistentConnection _persistentConnectionRabbitMq;
        private readonly IEventBusSubscriptionsManager _subscriptionsManager;
        private readonly int _retryCount;
        private readonly ILifetimeScope _scopeFactory;

        private IModel _consumerChannel;
        private string _queueName;

        public EventBus(
            ILogger<EventBus> logger,
            IPersistentConnection persistentConnectionRabbitMq,
            IEventBusSubscriptionsManager subscriptionsManager,
            ILifetimeScope scopeFactory, string queueName = null, int retryCount = 5
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _persistentConnectionRabbitMq = persistentConnectionRabbitMq ?? throw new ArgumentNullException(nameof(persistentConnectionRabbitMq));
            _subscriptionsManager = subscriptionsManager ?? throw new ArgumentNullException(nameof(subscriptionsManager));
            _scopeFactory = scopeFactory;

            _retryCount = retryCount;
            _queueName = queueName;
            _consumerChannel = CreateConsumerChannel();

            _subscriptionsManager.OnEventRemoved += OnEventRemoved;
        }

        private void OnEventRemoved(object sender, string eventName)
        {
            if (!_persistentConnectionRabbitMq.IsConnected)
            {
                _persistentConnectionRabbitMq.TryConnect();
            }

            using (var channel = _persistentConnectionRabbitMq.CreateModel())
            {
                channel.QueueUnbind(
                    queue: _queueName,
                    exchange: BrokerName,
                    routingKey: eventName
                );

                if (_subscriptionsManager.IsEmpty)
                {
                    _queueName = string.Empty;
                    _consumerChannel.Close();
                }
            }
        }

        public void Publish(Event @event)
        {
            if (!_persistentConnectionRabbitMq.IsConnected)
            {
                _persistentConnectionRabbitMq.TryConnect();
            }

            var policy = Policy.Handle<BrokerUnreachableException>()
                                .Or<SocketException>()
                                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                                {
                                    _logger.LogWarning(ex, $"Could not publish event: {@event.Id} after {time.TotalSeconds:n1}s {ex.Message}");
                                });

            var eventName = @event.GetType().Name;

            _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, eventName);

            using (var channel = _persistentConnectionRabbitMq.CreateModel())
            {
                _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);

                channel.ExchangeDeclare(exchange: BrokerName, type: ExchangeType.Direct);

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                policy.Execute(() =>
                {
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2; // persistent

                    _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);

                    channel.BasicPublish(
                        exchange: BrokerName,
                        routingKey: eventName,
                        mandatory: true,
                        basicProperties: properties,
                        body: body);
                });
            }
        }

        public void Subscribe<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>
        {
            var eventName = _subscriptionsManager.GetEventKey<TEvent>();
            DoInternalSubscription(eventName);

            _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TEventHandler).GetGenericTypeName());

            _subscriptionsManager.AddSubscription<TEvent, TEventHandler>();
            StartBasicConsume();
        }

        public void SubscribeDynamic<TEventHandler>(string eventName)
            where TEventHandler : IDynamicEventHandler
        {
            _logger.LogInformation("Subscribing to dynamic event {EventName} with {EventHandler}", eventName, typeof(TEventHandler).GetGenericTypeName());

            DoInternalSubscription(eventName);
            _subscriptionsManager.AddDynamicSubscription<TEventHandler>(eventName);
            StartBasicConsume();
        }

        public void UnsubscribeDynamic<TEventHandler>(string eventName)
            where TEventHandler : IDynamicEventHandler
        {
            _subscriptionsManager.RemoveDynamicSubscription<TEventHandler>(eventName);
        }

        public void Unsubscribe<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>
        {
            var eventName = _subscriptionsManager.GetEventKey<TEvent>();

            _logger.LogInformation("Unsubscribing from event {EventName}", eventName);

            _subscriptionsManager.RemoveSubscription<TEvent, TEventHandler>();
        }

        public void Dispose()
        {
            _consumerChannel?.Dispose();

            _subscriptionsManager.Clear();
        }

        /// <summary>
        /// Subscribes to queue
        /// </summary>
        private void DoInternalSubscription(string eventName)
        {
            var containsKey = _subscriptionsManager.HasSubscriptionsForEvent(eventName);

            // has subscription.. return
            if (containsKey)
                return;

            if (!_persistentConnectionRabbitMq.IsConnected)
            {
                _persistentConnectionRabbitMq.TryConnect();
            }

            using (var channel = _persistentConnectionRabbitMq.CreateModel())
            {
                // add queue
                channel.QueueBind(
                    queue: _queueName,
                    exchange: BrokerName,
                    routingKey: eventName);
            }
        }

        /// <summary>
        /// Starts consuming
        /// </summary>
        private void StartBasicConsume()
        {
            _logger.LogTrace("Starting RabbitMQ basic consume");

            if (_consumerChannel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                consumer.Received += ConsumerReceivedMessage;

                _consumerChannel.BasicConsume(
                    queue: _queueName,
                    autoAck: false,
                    consumer: consumer);
            }
            else
            {
                _logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
            }
        }

        /// <summary>
        /// When consumer receives message we have to process that message
        /// </summary>
        /// <returns></returns>
        private async Task ConsumerReceivedMessage(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

            try
            {
                if (message.ToLowerInvariant().Contains("throw-fake-exception"))
                {
                    throw new InvalidOperationException($"Fake exception requested: \"{message}\"");
                }

                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "----- ERROR Processing message \"{Message}\"", message);
            }

            // TODO
            // Even on exception we take the message off the queue.
            // in a REAL WORLD app this should be handled with a Dead Letter Exchange (DLX). 
            // For more information see: https://www.rabbitmq.com/dlx.html
            _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }

        /// <summary>
        /// Process event implements logic that goes through all subscriptions and triggers handling
        /// </summary>
        private async Task ProcessEvent(string eventName, string message)
        {
            _logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);

            if (_subscriptionsManager.HasSubscriptionsForEvent(eventName))
            {
                using (var scope = _scopeFactory.BeginLifetimeScope(ScopeName))
                {
                    var subscriptions = _subscriptionsManager.GetHandlersForEvent(eventName);

                    // go through all subscriptions and resolve handlers
                    foreach (var subscription in subscriptions)
                    {
                        // handle dynamic event handler
                        if (subscription.IsDynamic)
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType) as IDynamicEventHandler;

                            if (handler == null)
                                continue;

                            dynamic eventData = JObject.Parse(message);

                            await Task.Yield();
                            await handler.Handle(eventData);
                        }
                        // handle event handler
                        else
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType);

                            if (handler == null)
                                continue;

                            var eventType = _subscriptionsManager.GetEventTypeByName(eventName);
                            var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                            var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                            await Task.Yield();
                            await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                        }
                    }
                }
            }
            else
            {
                _logger.LogWarning("No subscription for RabbitMQ event: {EventName}", eventName);
            }
        }

        /// <summary>
        /// Creates consumer channel
        /// </summary>
        /// <returns></returns>
        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnectionRabbitMq.IsConnected)
            {
                _persistentConnectionRabbitMq.TryConnect();
            }

            _logger.LogTrace("Creating RabbitMQ consumer channel");

            var channel = _persistentConnectionRabbitMq.CreateModel();

            channel.ExchangeDeclare(exchange: BrokerName, ExchangeType.Direct);

            channel.QueueDeclare(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.CallbackException += (sender, ea) =>
            {
                _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel");

                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
                StartBasicConsume();
            };

            return channel;
        }

    }
}
