using FluentValidation;
using ItemService.Business.Validators;

namespace ItemService.Business.Commands.Items.UpdateItem
{
    public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
    {
        public UpdateItemCommandValidator()
        {
            RuleFor(x => x.Item).SetValidator(new ItemValidator());
            RuleFor(x => x.Item.Category).SetValidator(new CategoryValidator());
            RuleForEach(x => x.Item.Ingredients).SetValidator(new IngredientValidator());
            RuleForEach(x => x.Item.Allergens).SetValidator(new AllergenValidator());
        }
    }
}