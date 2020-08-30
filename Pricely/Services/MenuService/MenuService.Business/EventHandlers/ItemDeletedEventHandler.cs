using EventBus.Infrastructure.Interfaces;
using EventBus.Infrastructure.Models;
using System;
using System.Threading.Tasks;

namespace MenuService.Business.EventHandlers
{
    public class ItemDeletedEvent : Event
    {
        public Guid Id { get; set; }
    }

    public class ItemDeletedEventHandler : IEventHandler<ItemDeletedEvent>
    {
        public Task Handle(ItemDeletedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
