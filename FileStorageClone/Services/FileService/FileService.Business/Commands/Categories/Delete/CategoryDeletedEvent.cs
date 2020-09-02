using System;
using EventBus.Infrastructure.Models;

namespace ItemService.Business.Commands.Categories.Delete
{
    /// <summary>
    /// Event that carries information about deleted event
    /// </summary>
    public class CategoryDeletedEvent : Event
    {
        public CategoryDeletedEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
