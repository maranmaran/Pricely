using EventBus.Infrastructure.Models;
using ItemService.Domain.Entities;

namespace ItemService.Business.Commands.Ingredients.Update
{
    public class IngredientUpdatedEvent : Event
    {
        public IngredientUpdatedEvent(Ingredient ingredient)
        {
            Ingredient = ingredient;
        }

        public Ingredient Ingredient { get; set; }
    }
}
