using FluentValidation;
using MenuService.Business.Validators;

namespace MenuService.Business.Commands.Menu.Create
{
    public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
    {
        public CreateMenuCommandValidator()
        {
            RuleFor(x => x.Menu).SetValidator(new MenuValidator());
        }
    }
}