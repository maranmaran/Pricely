using FluentValidation;
using IdentityService.Persistence.DTOModels;

namespace IdentityService.Business.Validators
{
    public class ItemValidator : AbstractValidator<ItemDto>
    {
        public ItemValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
