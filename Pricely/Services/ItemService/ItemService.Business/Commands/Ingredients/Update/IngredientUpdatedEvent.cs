using EventBus.Infrastructure.Models;
using ItemService.Domain.Entities;
using ItemService.Persistence.DTOModels;

namespace ItemService.Business.Commands.Ingredients.Update
{
    public class IngredientUpdatedEvent : Event
    {
        public IngredientUpdatedEvent(IngredientDto ingredient)
        {
            Ingredient = ingredient;
        }

        public IngredientDto Ingredient { get; set; }
    }
}
