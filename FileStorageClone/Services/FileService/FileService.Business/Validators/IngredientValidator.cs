using FluentValidation;
using ItemService.Persistence.DTOModels;

namespace ItemService.Business.Validators
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