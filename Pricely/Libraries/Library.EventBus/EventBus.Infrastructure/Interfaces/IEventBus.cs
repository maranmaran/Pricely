using EventBus.Infrastructure.Models;

namespace EventBus.Infrastructure.Interfaces
{
    /// <summary>
    /// Handles event bus interaction
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Publishes the specified event.
        /// </summary>
        /// <param name="event">The event.</param>
        void Publish(Event @event);

        /// <summary>
        /// Subscribes this instance.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <typeparam name="TEventHandler">The type of the event handler.</typeparam>
        void Subscribe<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>;

        /// <summary>
        /// Subscribes the dynamic.
        /// </summary>
        /// <typeparam name="TEventHandler">The type of the event handler.</typeparam>
        /// <param name="eventName">Name of the event.</param>
        void SubscribeDynamic<TEventHandler>(string eventName)
            where TEventHandler : IDynamicEventHandler;

        /// <summary>
        /// Unsubscribes the dynamic.
        /// </summary>
        /// <typeparam name="TEventHandler">The type of the event handler.</typeparam>
        /// <param name="eventName">Name of the event.</param>
        void UnsubscribeDynamic<TEventHandler>(string eventName)
            where TEventHandler : IDynamicEventHandler;

        /// <summary>
        /// Unsubscribes this instance.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <typeparam name="TEventHandler">The type of the event handler.</typeparam>
        void Unsubscribe<TEvent, TEventHandler>()
            where TEventHandler : IEventHandler<TEvent>
            where TEvent : Event;
    }
}
