using FluentValidation;
using MenuService.Business.Validators;

namespace MenuService.Business.Commands.Menus.Create
{
    public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
    {
        public CreateMenuCommandValidator()
        {
            RuleFor(x => x.Menu).SetValidator(new MenuValidator());
        }
    }
}