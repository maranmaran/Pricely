using EventBus.Infrastructure.Models;
using ItemService.Persistence.DTOModels;

namespace ItemService.Business.Commands.Items.Update
{
    public class ItemUpdatedEvent : Event
    {
        public ItemUpdatedEvent(ItemDto item)
        {
            Item = item;
        }

        public ItemDto Item { get; set; }
    }
}
