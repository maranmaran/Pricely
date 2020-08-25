using FluentValidation;
using ItemService.Persistence.DTOModels;

namespace ItemService.Business.Validators
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