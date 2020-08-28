using FluentValidation;
using ItemService.Business.Validators;

namespace ItemService.Business.Commands.Categories.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Category).SetValidator(new CategoryValidator());
        }
    }
}