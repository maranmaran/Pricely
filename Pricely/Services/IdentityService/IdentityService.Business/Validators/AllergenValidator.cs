using FluentValidation;
using IdentityService.Persistence.DTOModels;

namespace IdentityService.Business.Validators
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