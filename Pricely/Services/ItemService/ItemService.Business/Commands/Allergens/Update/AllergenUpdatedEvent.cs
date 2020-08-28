using EventBus.Infrastructure.Models;
using ItemService.Domain.Entities;

namespace ItemService.Business.Commands.Allergens.Update
{
    public class AllergenUpdatedEvent : Event
    {
        public AllergenUpdatedEvent(Allergen allergen)
        {
            Allergen = allergen;
        }

        public Allergen Allergen { get; set; }
    }
}
