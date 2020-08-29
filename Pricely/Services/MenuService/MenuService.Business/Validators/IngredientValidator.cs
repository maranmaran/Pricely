using FluentValidation;
using MenuService.Persistence.DTOModels;

namespace MenuService.Business.Validators
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