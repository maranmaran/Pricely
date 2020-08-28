using System;
using EventBus.Infrastructure.Models;

namespace ItemService.Business.Commands.Items.Delete
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
