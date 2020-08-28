using ItemService.Persistence.DTOModels;
using MediatR;

namespace ItemService.Business.Commands.Allergens.Update
{
    public class UpdateAllergenCommand : IRequest<Unit>
    {
        public UpdateAllergenCommand(AllergenDto allergen)
        {
            Allergen = allergen;
        }

        public AllergenDto Allergen { get; set; }
    }
}
