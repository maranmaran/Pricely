using ItemService.Persistence.DTOModels;
using MediatR;

namespace ItemService.Business.Commands.Ingredients.Update
{
    public class UpdateIngredientCommand : IRequest<Unit>
    {
        public UpdateIngredientCommand(IngredientDto ingredient)
        {
            Ingredient = ingredient;
        }

        public IngredientDto Ingredient { get; set; }
    }
}
