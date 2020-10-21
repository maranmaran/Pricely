using EventBus.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace EventBus.Infrastructure.Interfaces
{

    /// <summary>
    /// Handles all event bus subscriptions
    /// </summary>
    /// <remarks>
    /// Given persistence layer of subscription manager depends on implementation
    /// </remarks>
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnEventRemoved;

        /// <summary>
        /// Adds the dynamic subscription.
        /// </summary>
        /// <typeparam name="TEventHandler">The type of the event handler.</typeparam>
        /// <param name="eventName">Name of the event.</param>
        void AddDynamicSubscription<TEventHandler>(string eventName)
            where TEventHandler : IDynamicEventHandler;

        /// <summary>
        /// Adds the subscription.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <typeparam name="TEventHandler">The type of the event handler.</typeparam>
        void AddSubscription<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>;

        /// <summary>
        /// Removes the subscription.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <typeparam name="TEventHandler">The type of the event handler.</typeparam>
        void RemoveSubscription<TEvent, TEventHandler>()
            where TEventHandler : IEventHandler<TEvent>
            where TEvent : Event;

        /// <summary>
        /// Removes the dynamic subscription.
        /// </summary>
        /// <typeparam name="TEventHandler">The type of the event handler.</typeparam>
        /// <param name="eventName">Name of the event.</param>
        void RemoveDynamicSubscription<TEventHandler>(string eventName)
            where TEventHandler : IDynamicEventHandler;

        /// <summary>
        /// Determines whether [has subscriptions for event].
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <returns>
        ///   <c>true</c> if [has subscriptions for event]; otherwise, <c>false</c>.
        /// </returns>
        bool HasSubscriptionsForEvent<TEvent>() where TEvent : Event;

        /// <summary>
        /// Determines whether [has subscriptions for event] [the specified event name].
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <returns>
        ///   <c>true</c> if [has subscriptions for event] [the specified event name]; otherwise, <c>false</c>.
        /// </returns>
        bool HasSubscriptionsForEvent(string eventName);

        /// <summary>
        /// Gets the name of the event type by.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <returns>Event type</returns>
        Type GetEventTypeByName(string eventName);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets the handlers for event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <returns>List of <see cref="SubscriptionInfo"/></returns>
        IEnumerable<SubscriptionInfo> GetHandlersForEvent<TEvent>() where TEvent : Event;

        /// <summary>
        /// Gets the handlers for event.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <returns>List of <see cref="SubscriptionInfo"/></returns>
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);

        /// <summary>
        /// Gets the event key.
        /// </summary>
        /// <typeparam name="T">Generic event type</typeparam>
        /// <returns>Event key descriptor</returns>
        /// <example>Event type name</example>
        string GetEventKey<T>();
    }
}
