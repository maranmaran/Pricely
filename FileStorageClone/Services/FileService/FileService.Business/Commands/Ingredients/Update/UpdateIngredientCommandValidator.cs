using FluentValidation;
using ItemService.Business.Validators;

namespace ItemService.Business.Commands.Ingredients.Update
{
    public class UpdateIngredientCommandValidator : AbstractValidator<UpdateIngredientCommand>
    {
        public UpdateIngredientCommandValidator()
        {
            RuleFor(x => x.Ingredient).SetValidator(new IngredientValidator());
        }
    }
}