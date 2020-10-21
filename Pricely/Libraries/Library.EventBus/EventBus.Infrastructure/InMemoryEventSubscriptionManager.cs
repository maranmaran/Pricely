using EventBus.Infrastructure.Interfaces;
using EventBus.Infrastructure.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace EventBus.Infrastructure
{

    /// <summary>
    /// Implements subscription management in memory
    /// </summary>
    /// <seealso cref="IEventBusSubscriptionsManager" />
    public partial class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        private readonly ConcurrentDictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public event EventHandler<string> OnEventRemoved;

        public InMemoryEventBusSubscriptionsManager()
        {
            _handlers = new ConcurrentDictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();
        }

        public bool IsEmpty => !_handlers.Keys.Any();
        public void Clear() => _handlers.Clear();

        public void AddDynamicSubscription<TEventHandler>(string eventName)
            where TEventHandler : IDynamicEventHandler
        {
            DoAddSubscription(typeof(TEventHandler), eventName, isDynamic: true);
        }

        public void AddSubscription<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>
        {
            var eventName = GetEventKey<TEvent>();

            DoAddSubscription(typeof(TEventHandler), eventName, isDynamic: false);

            if (!_eventTypes.Contains(typeof(TEvent)))
            {
                _eventTypes.Add(typeof(TEvent));
            }
        }

        private void DoAddSubscription(Type handlerType, string eventName, bool isDynamic)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.TryAdd(eventName, new List<SubscriptionInfo>());
            }

            if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            }

            _handlers[eventName]
                .Add(isDynamic ? SubscriptionInfo.Dynamic(handlerType) : SubscriptionInfo.Typed(handlerType));
        }


        public void RemoveDynamicSubscription<TEventHandler>(string eventName)
            where TEventHandler : IDynamicEventHandler
        {
            var handlerTRemove = FindDynamicSubscriptionToRemove<TEventHandler>(eventName);

            DoRemoveHandler(eventName, handlerTRemove);
        }


        public void RemoveSubscription<TEvent, TEventHandler>()
            where TEventHandler : IEventHandler<TEvent>
            where TEvent : Event
        {
            var handlerToRemove = FindSubscriptionToRemove<TEvent, TEventHandler>();
            var eventName = GetEventKey<TEvent>();

            DoRemoveHandler(eventName, handlerToRemove);
        }


        private void DoRemoveHandler(string eventName, SubscriptionInfo subsToRemove)
        {
            // validate parameter
            if (subsToRemove == null)
                return;

            // validate event handler exists
            if (_handlers[eventName].Any())
                return;

            // do remove
            _handlers[eventName].Remove(subsToRemove);

            // get type and remove it
            var eventType = _eventTypes.FirstOrDefault(e => e.Name == eventName);
            if (eventType != null)
            {
                _eventTypes.Remove(eventType);
            }

            RaiseOnEventRemoved(eventName);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<TEvent>() where TEvent : Event
        {
            var key = GetEventKey<TEvent>();

            return GetHandlersForEvent(key);
        }
        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => _handlers[eventName];

        private void RaiseOnEventRemoved(string eventName)
        {
            var handler = OnEventRemoved;

            handler?.Invoke(this, eventName);
        }


        private SubscriptionInfo FindDynamicSubscriptionToRemove<TEventHandler>(string eventName)
            where TEventHandler : IDynamicEventHandler
        {
            return DoFindSubscriptionToRemove(eventName, typeof(TEventHandler));
        }


        private SubscriptionInfo FindSubscriptionToRemove<TEvent, TEventHandler>()
             where TEvent : Event
             where TEventHandler : IEventHandler<TEvent>
        {
            var eventName = GetEventKey<TEvent>();
            return DoFindSubscriptionToRemove(eventName, typeof(TEventHandler));
        }

        private SubscriptionInfo DoFindSubscriptionToRemove(string eventName, Type handlerType)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                return null;
            }

            return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);

        }

        public bool HasSubscriptionsForEvent<TEvent>() where TEvent : Event
        {
            var key = GetEventKey<TEvent>();

            return HasSubscriptionsForEvent(key);
        }
        public bool HasSubscriptionsForEvent(string eventName)
            => _handlers.ContainsKey(eventName);

        public Type GetEventTypeByName(string eventName)
            => _eventTypes.SingleOrDefault(t => t.Name == eventName);

        public string GetEventKey<TEvent>()
            => typeof(TEvent).Name;
    }
}
