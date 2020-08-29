using FluentValidation;
using MenuService.Persistence.DTOModels;

namespace MenuService.Business.Validators
{
    public class AllergenValidator : AbstractValidator<AllergenDto>
    {
        public AllergenValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}