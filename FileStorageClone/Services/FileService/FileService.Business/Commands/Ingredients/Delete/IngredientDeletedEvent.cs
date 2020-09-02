using System;
using EventBus.Infrastructure.Models;

namespace ItemService.Business.Commands.Ingredients.Delete
{
    /// <summary>
    /// Event that carries information about deleted event
    /// </summary>
    public class IngredientDeletedEvent : Event
    {
        public IngredientDeletedEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
