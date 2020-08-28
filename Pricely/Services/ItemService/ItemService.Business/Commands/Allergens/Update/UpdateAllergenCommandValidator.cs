using FluentValidation;
using ItemService.Business.Validators;

namespace ItemService.Business.Commands.Allergens.Update
{
    public class UpdateAllergenCommandValidator : AbstractValidator<UpdateAllergenCommand>
    {
        public UpdateAllergenCommandValidator()
        {
            RuleFor(x => x.Allergen).SetValidator(new AllergenValidator());
        }
    }
}