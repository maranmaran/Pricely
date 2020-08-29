using FluentValidation;
using MenuService.Persistence.DTOModels;

namespace MenuService.Business.Validators
{
    public class MenuValidator : AbstractValidator<MenuDto>
    {
        public MenuValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}