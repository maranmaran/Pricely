using System;
using ItemService.Persistence.DTOModels;
using MediatR;

namespace ItemService.Business.Commands.Ingredients.Create
{
    public class CreateIngredientCommand : IRequest<Guid>
    {
        public CreateIngredientCommand(IngredientDto ingredient)
        {
            Ingredient = ingredient;
        }

        public IngredientDto Ingredient { get; set; }
    }
}
