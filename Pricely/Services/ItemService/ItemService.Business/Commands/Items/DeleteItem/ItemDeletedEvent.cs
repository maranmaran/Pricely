using EventBus.Infrastructure.Models;
using System;

namespace ItemService.Business.Commands.Items.DeleteItem
{
    /// <summary>
    /// Event that carries information about deleted event
    /// </summary>
    public class ItemDeletedEvent : Event
    {
        public ItemDeletedEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
