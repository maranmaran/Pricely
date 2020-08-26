using FluentValidation;
using IdentityService.Business.Validators;

namespace IdentityService.Business.Commands.Items.CreateItem
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