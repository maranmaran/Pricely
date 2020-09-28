using EventBus.Infrastructure.Interfaces;
using EventBus.Infrastructure.Models;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ItemDeletedEventHandler> _logger;

        public ItemDeletedEventHandler(ILogger<ItemDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(ItemDeletedEvent @event)
        {
            _logger.LogInformation($"Handling event {nameof(ItemDeletedEvent)}");
            _logger.LogInformation($"Event data: ItemId - {@event.Id}");
        }
    }
}
