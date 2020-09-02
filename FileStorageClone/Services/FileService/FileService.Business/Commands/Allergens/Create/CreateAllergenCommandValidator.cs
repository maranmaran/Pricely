using FluentValidation;
using ItemService.Business.Validators;

namespace ItemService.Business.Commands.Allergens.Create
{
    public class CreateAllergenCommandValidator : AbstractValidator<CreateAllergenCommand>
    {
        public CreateAllergenCommandValidator()
        {
            RuleFor(x => x.Allergen).SetValidator(new AllergenValidator());
        }
    }
}