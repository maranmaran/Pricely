using EventBus.Infrastructure.Models;

namespace EventBus.Infrastructure.Interfaces
{
    public interface IEventBus
    {
        void Publish(Event @event);

        void Subscribe<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IEventHandler<TEvent>;

        void SubscribeDynamic<TEventHandler>(string eventName)
            where TEventHandler : IDynamicEventHandler;

        void UnsubscribeDynamic<TEventHandler>(string eventName)
            where TEventHandler : IDynamicEventHandler;

        void Unsubscribe<TEvent, TEventHandler>()
            where TEventHandler : IEventHandler<TEvent>
            where TEvent : Event;
    }
}
