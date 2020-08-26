using FluentValidation;
using IdentityService.Persistence.DTOModels;

namespace IdentityService.Business.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}