using FluentValidation;
using IdentityService.Persistence.DTOModels;

namespace IdentityService.Business.Validators
{
    public class IngredientValidator : AbstractValidator<IngredientDto>
    {
        public IngredientValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}