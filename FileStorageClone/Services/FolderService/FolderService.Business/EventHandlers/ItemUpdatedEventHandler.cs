using EventBus.Infrastructure.Interfaces;
using EventBus.Infrastructure.Models;
using MenuService.Persistence.DTOModels;
using System.Threading.Tasks;

namespace MenuService.Business.EventHandlers
{
    public class ItemUpdatedEvent : Event
    {
        public ItemDto Item { get; set; }

    }

    public class ItemUpdatedEventHandler : IEventHandler<ItemUpdatedEvent>
    {
        public Task Handle(ItemUpdatedEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}
