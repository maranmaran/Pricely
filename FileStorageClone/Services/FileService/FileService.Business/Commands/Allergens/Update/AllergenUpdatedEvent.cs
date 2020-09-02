using EventBus.Infrastructure.Models;
using ItemService.Persistence.DTOModels;

namespace ItemService.Business.Commands.Allergens.Update
{
    public class AllergenUpdatedEvent : Event
    {
        public AllergenUpdatedEvent(AllergenDto allergen)
        {
            Allergen = allergen;
        }

        public AllergenDto Allergen { get; set; }
    }
}
