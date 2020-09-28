using EventBus.Infrastructure.Interfaces;
using EventBus.Infrastructure.Models;
using MenuService.Persistence.DTOModels;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MenuService.Business.EventHandlers
{
    public class ItemUpdatedEvent : Event
    {
        public ItemDto Item { get; set; }

    }

    public class ItemUpdatedEventHandler : IEventHandler<ItemUpdatedEvent>
    {
        private readonly ILogger<ItemUpdatedEventHandler> _logger;

        public ItemUpdatedEventHandler(ILogger<ItemUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(ItemUpdatedEvent @event)
        {
            _logger.LogInformation($"Handling event {nameof(ItemUpdatedEvent)}");
            _logger.LogInformation($"Event data: ItemId - {@event.Id}");
        }
    }
}
