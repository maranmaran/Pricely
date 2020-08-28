using System;
using ItemService.Persistence.DTOModels;
using MediatR;

namespace ItemService.Business.Commands.Allergens.Create
{
    public class CreateAllergenCommand : IRequest<Guid>
    {
        public CreateAllergenCommand(AllergenDto allergen)
        {
            Allergen = allergen;
        }

        public AllergenDto Allergen { get; set; }
    }
}
