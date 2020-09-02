using EventBus.Infrastructure.Models;
using System.Threading.Tasks;

namespace EventBus.Infrastructure.Interfaces
{
    public interface IEventHandler
    {
    }

    public interface IEventHandler<in TEvent> : IEventHandler
        where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    public interface IDynamicEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
