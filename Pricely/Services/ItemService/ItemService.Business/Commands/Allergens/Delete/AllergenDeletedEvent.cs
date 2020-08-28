using System;
using EventBus.Infrastructure.Models;

namespace ItemService.Business.Commands.Allergens.Delete
{
    /// <summary>
    /// Event that carries information about deleted event
    /// </summary>
    public class AllergenDeletedEvent : Event
    {
        public AllergenDeletedEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
