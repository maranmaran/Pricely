using FluentValidation;
using ItemService.Business.Validators;

namespace ItemService.Business.Commands.Ingredients.Create
{
    public class CreateIngredientCommandValidator : AbstractValidator<CreateIngredientCommand>
    {
        public CreateIngredientCommandValidator()
        {
            RuleFor(x => x.Ingredient).SetValidator(new IngredientValidator());
        }
    }
}