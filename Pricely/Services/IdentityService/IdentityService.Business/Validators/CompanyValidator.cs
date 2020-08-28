using FluentValidation;
using IdentityService.Persistence.DTOModels;

namespace IdentityService.Business.Validators
{
    public class CompanyValidator : AbstractValidator<CompanyDto>
    {
        public CompanyValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}