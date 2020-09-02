using FluentValidation;
using ItemService.Business.Validators;

namespace ItemService.Business.Commands.Items.Create
{
    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator()
        {
            RuleFor(x => x.Item).SetValidator(new ItemValidator());
            RuleFor(x => x.Item.Category).SetValidator(new CategoryValidator());
            RuleForEach(x => x.Item.Ingredients).SetValidator(new IngredientValidator());
            RuleForEach(x => x.Item.Allergens).SetValidator(new AllergenValidator());
        }
    }
}