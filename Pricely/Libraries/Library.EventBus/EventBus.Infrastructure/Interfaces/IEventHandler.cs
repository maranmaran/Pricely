using EventBus.Infrastructure.Models;
using System.Threading.Tasks;

namespace EventBus.Infrastructure.Interfaces
{
    /// <summary>
    /// Handles event 
    /// </summary>
    /// <remarks>Strongly typed</remarks>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public interface IEventHandler<in TEvent>
        where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    /// <summary>
    /// Handles event
    /// </summary>
    /// <remarks>Dynamic event data</remarks>
    public interface IDynamicEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
