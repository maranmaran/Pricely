using EventBus.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace EventBus.Infrastructure.Interfaces
{
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnEventRemoved;

        void AddDynamicSubscription<TEventHandler>(string eventName)
            where TEventHandler : IDynamicEventHandler;

        void AddSubscription<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>;

        void RemoveSubscription<TEvent, TEventHandler>()
            where TEventHandler : IEventHandler<TEvent>
            where TEvent : Event;
        void RemoveDynamicSubscription<TEventHandler>(string eventName)
            where TEventHandler : IDynamicEventHandler;

        bool HasSubscriptionsForEvent<TEvent>() where TEvent : Event;

        bool HasSubscriptionsForEvent(string eventName);

        Type GetEventTypeByName(string eventName);

        void Clear();

        IEnumerable<SubscriptionInfo> GetHandlersForEvent<TEvent>() where TEvent : Event;

        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);

        string GetEventKey<T>();
    }
}
