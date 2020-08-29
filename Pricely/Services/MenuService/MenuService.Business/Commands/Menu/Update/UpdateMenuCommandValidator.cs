using FluentValidation;
using MenuService.Business.Validators;

namespace MenuService.Business.Commands.Menus.Update
{
    public class UpdateMenuCommandValidator : AbstractValidator<UpdateMenuCommand>
    {
        public UpdateMenuCommandValidator()
        {
            RuleFor(x => x.Menu).SetValidator(new MenuValidator());
        }
    }
}