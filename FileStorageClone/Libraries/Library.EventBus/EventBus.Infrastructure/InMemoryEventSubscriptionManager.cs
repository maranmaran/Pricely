using EventBus.Infrastructure.Interfaces;
using EventBus.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventBus.Infrastructure
{
    public partial class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public event EventHandler<string> OnEventRemoved;

        public InMemoryEventBusSubscriptionsManager()
        {
            _handlers = new Dictionary<string, List<SubscriptionInfo>>();
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
                _handlers.Add(eventName, new List<SubscriptionInfo>());
            }

            if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            }

            if (isDynamic)
            {
                _handlers[eventName].Add(SubscriptionInfo.Dynamic(handlerType));
            }
            else
            {
                _handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
            }
        }


        public void RemoveDynamicSubscription<TEventHandler>(string eventName)
            where TEventHandler : IDynamicEventHandler
        {
            var handlerTEventoRemove = FindDynamicSubscriptionTEventoRemove<TEventHandler>(eventName);
            DoRemoveHandler(eventName, handlerTEventoRemove);
        }


        public void RemoveSubscription<TEvent, TEventHandler>()
            where TEventHandler : IEventHandler<TEvent>
            where TEvent : Event
        {
            var handlerTEventoRemove = FindSubscriptionTEventoRemove<TEvent, TEventHandler>();
            var eventName = GetEventKey<TEvent>();
            DoRemoveHandler(eventName, handlerTEventoRemove);
        }


        private void DoRemoveHandler(string eventName, SubscriptionInfo subsTEventoRemove)
        {
            if (subsTEventoRemove != null)
            {
                _handlers[eventName].Remove(subsTEventoRemove);
                if (!_handlers[eventName].Any())
                {
                    _handlers.Remove(eventName);
                    var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
                    if (eventType != null)
                    {
                        _eventTypes.Remove(eventType);
                    }
                    RaiseOnEventRemoved(eventName);
                }

            }
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


        private SubscriptionInfo FindDynamicSubscriptionTEventoRemove<TEventHandler>(string eventName)
            where TEventHandler : IDynamicEventHandler
        {
            return DoFindSubscriptionTEventoRemove(eventName, typeof(TEventHandler));
        }


        private SubscriptionInfo FindSubscriptionTEventoRemove<TEvent, TEventHandler>()
             where TEvent : Event
             where TEventHandler : IEventHandler<TEvent>
        {
            var eventName = GetEventKey<TEvent>();
            return DoFindSubscriptionTEventoRemove(eventName, typeof(TEventHandler));
        }

        private SubscriptionInfo DoFindSubscriptionTEventoRemove(string eventName, Type handlerType)
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
        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

        public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);

        public string GetEventKey<TEvent>()
        {
            return typeof(TEvent).Name;
        }
    }
}
