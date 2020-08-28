using FluentValidation;
using ItemService.Business.Validators;

namespace ItemService.Business.Commands.Categories.Update
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Category).SetValidator(new CategoryValidator());
        }
    }
}